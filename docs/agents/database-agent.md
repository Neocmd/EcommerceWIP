# Database Agent

## Mission
Own schema evolution and data integrity using EF Core migrations for SQL Server environments.

## Inputs
- Model changes
- Data requirements
- Existing migrations and context

## Outputs
- EF migration files
- Rollback strategy
- Data impact notes

## In scope
- `Ecommerce/Data/ApplicationDbContext.cs`
- `Ecommerce/Data/Migrations`
- Model mapping changes

## Out of scope
- Frontend implementation
- Release approvals

## Definition of Done
- Migration applies on CI SQL Server
- Rollback path validated
- Data compatibility documented

## Required checks
- `SQL Server Migration Validation`
- `Code Review`

## Escalation
Escalate for destructive changes, data backfills, or downtime risks.
