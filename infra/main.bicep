// ============================================================================
// MusclePlus 4000 – Azure Infrastructure
// All resources stay within the Azure 12-month free tier:
//   • App Service Plan B1 (Linux)  – 750 h/month free for 12 months
//   • App Service (.NET 10)        – included in the plan above
//   • PostgreSQL Flexible Server   – Standard_B1ms, 32 GB free for 12 months
//   • Static Web App               – Free tier (always free)
// ============================================================================

@description('Primary Azure region for all resources.')
param location string = resourceGroup().location

@description('Azure region for the Static Web App (limited availability).')
@allowed(['centralus', 'eastus2', 'eastasia', 'westeurope', 'westus2'])
param swaLocation string = 'eastus2'

@description('Short application name prefix used in every resource name (max 16 chars).')
@maxLength(16)
param appName string = 'muscleplus4000'

@description('PostgreSQL administrator login name.')
param dbAdminUsername string = 'pgadmin'

@description('PostgreSQL administrator password.')
@secure()
param dbAdminPassword string

// ---------- Name helpers ----------------------------------------------------
var suffix     = take(uniqueString(resourceGroup().id), 6)
var planName   = '${appName}-plan-${suffix}'
var apiAppName = '${appName}-api-${suffix}'
var dbName_res = '${appName}-db-${suffix}'
var swaName    = '${appName}-web-${suffix}'
var dbName     = 'workout'

// ── App Service Plan (Linux B1 – 12 months free) ────────────────────────────
resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: planName
  location: location
  kind: 'linux'
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
  properties: {
    reserved: true   // required for Linux
  }
}

// ── App Service – .NET 10 API ────────────────────────────────────────────────
resource apiApp 'Microsoft.Web/sites@2023-12-01' = {
  name: apiAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|10.0'
      minTlsVersion: '1.2'
      http20Enabled: true
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Production'
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          // Npgsql connection string; SSL is required by Azure PostgreSQL.
          connectionString: 'Host=${dbServer.properties.fullyQualifiedDomainName};Database=${dbName};Username=${dbAdminUsername};Password=${dbAdminPassword};SSL Mode=Require;Trust Server Certificate=true'
          type: 'PostgreSQL'
        }
      ]
    }
  }
}

// ── PostgreSQL Flexible Server (Standard_B1ms, 32 GB – 12 months free) ──────
resource dbServer 'Microsoft.DBforPostgreSQL/flexibleServers@2024-08-01' = {
  name: dbName_res
  location: location
  sku: {
    name: 'Standard_B1ms'
    tier: 'Burstable'
  }
  properties: {
    administratorLogin: dbAdminUsername
    administratorLoginPassword: dbAdminPassword
    version: '17'
    storage: {
      storageSizeGB: 32
    }
    backup: {
      backupRetentionDays: 7
      geoRedundantBackup: 'Disabled'
    }
    highAvailability: {
      mode: 'Disabled'
    }
    network: {
      publicNetworkAccess: 'Enabled'
    }
  }
}

// Allow all Azure-internal traffic (startIp == endIp == 0.0.0.0 is the
// special "Allow Azure services" rule for Flexible Server).
resource dbFirewallAzureServices 'Microsoft.DBforPostgreSQL/flexibleServers/firewallRules@2024-08-01' = {
  parent: dbServer
  name: 'AllowAzureServices'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

// Create the workout database
resource workoutDatabase 'Microsoft.DBforPostgreSQL/flexibleServers/databases@2024-08-01' = {
  parent: dbServer
  name: dbName
  properties: {
    charset: 'UTF8'
    collation: 'en_US.utf8'
  }
}

// ── Static Web App (Free tier – always free) ─────────────────────────────────
resource staticWebApp 'Microsoft.Web/staticSites@2023-12-01' = {
  name: swaName
  location: swaLocation
  sku: {
    name: 'Free'
    tier: 'Free'
  }
  properties: {
    stagingEnvironmentPolicy: 'Enabled'
    allowConfigFileUpdates: true
    buildProperties: {
      skipGithubActionWorkflowGeneration: true   // we manage the workflow ourselves
    }
  }
}

// ── Outputs (use these to fill in GitHub Actions secrets) ────────────────────
@description('App Service name → set as AZURE_API_APP_NAME secret.')
output apiAppName string = apiApp.name

@description('API public URL.')
output apiUrl string = 'https://${apiApp.properties.defaultHostName}'

@description('Static Web App name → used to retrieve the SWA deployment token.')
output swaName string = staticWebApp.name

@description('Static Web App default hostname.')
output swaHostname string = staticWebApp.properties.defaultHostname

@description('PostgreSQL server FQDN (for reference).')
output dbServerFqdn string = dbServer.properties.fullyQualifiedDomainName

@description('PostgreSQL server name (for running init scripts).')
output dbServerName string = dbServer.name

