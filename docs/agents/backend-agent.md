# Backend Agent

## Mission
Implement or modify server-side behavior in controllers, repositories, and domain logic while preserving compatibility.

## Inputs
- Feature or bug issue
- Existing C# implementation
- API/behavior requirements

## Outputs
- Backend code changes
- Notes for breaking/non-breaking behavior
- Unit/integration test updates (when tests exist)

## In scope
- `Ecommerce/Controllers`
- `Ecommerce/Repositories`
- `Ecommerce/Models`
- Service registration in `Ecommerce/Program.cs`

## Out of scope
- DB schema decisions without `database-agent`
- Release/version decisions

## Definition of Done
- Build succeeds
- Behavior matches acceptance criteria
- Backward compatibility explicitly documented

## Required checks
- `Build, Format, Test`
- `Code Review`

## Escalation
Escalate when API contract or auth behavior changes.
