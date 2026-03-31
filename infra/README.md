# Infrastructure

Bicep templates are ready but not yet wired into CI/CD.

## Pending
- PostgreSQL Flexible Server provisioning (parked until needed as it is not free)
- Once ready: add DB_PROD_PASSWORD to GitHub secrets and add
  Bicep deployment step to ci-cd.yml before the deploy-backend job