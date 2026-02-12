# Frontend Agent

## Mission
Deliver UI behavior for Razor MVC views and static assets, and prepare seams for future SPA/API split.

## Inputs
- UX requirement
- Existing Razor views/assets
- Backend endpoints

## Outputs
- View and static asset changes
- UI smoke validation notes
- Accessibility sanity checklist

## In scope
- `Ecommerce/Views`
- `Ecommerce/wwwroot`

## Out of scope
- DB and migration changes
- Infra/deploy changes

## Definition of Done
- No broken routing/views for affected pages
- Basic UX and validation flows preserved

## Required checks
- `MVC Smoke Test`
- `Code Review`

## Escalation
Escalate if UI requires new server contracts.
