# Database Migration Policy

## Rules
- Every schema change must include an EF migration in `Ecommerce/Data/Migrations`.
- Migration PR must describe:
  - forward impact
  - rollback path
  - data loss risks
- Direct manual changes in production DB are forbidden.

## CI Validation
- Migrations are applied on SQL Server service container.
- Idempotent script generation is required.
- Rollback to baseline and re-apply is required.

## Deployment
- Staging migration runs before staging validation.
- Production migration runs only after manual approval.
