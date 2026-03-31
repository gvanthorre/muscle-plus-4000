using 'main.bicep'

// ── Required ──────────────────────────────────────────────────────────────────
// Pass dbAdminPassword on the CLI so it never touches source control:
//   az deployment group create ... --parameters dbAdminPassword='<YOUR_PASS>'

// ── Optional overrides ────────────────────────────────────────────────────────
param appName        = 'muscleplus4000'
param dbAdminUsername = 'pgadmin'

// Change swaLocation if eastus2 is unavailable in your subscription.
// Valid values: centralus | eastus2 | eastasia | westeurope | westus2
param swaLocation = 'westeurope'

param dbAdminPassword = ''

