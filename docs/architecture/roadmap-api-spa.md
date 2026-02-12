# Roadmap: Progressive API + SPA

## Current State
- ASP.NET Core MVC with Razor views.
- Repository pattern + EF Core + Identity.

## Target State
- API-first backend contracts for new modules.
- SPA frontend introduced incrementally.
- Existing Razor flows remain compatible during transition.

## Phases
1. Contract extraction
- Introduce API endpoints for new frontend features first.
- Keep existing MVC routes operational.

2. Frontend coexistence
- Serve SPA for selected routes/features.
- Maintain shared auth/session strategy.

3. Consolidation
- Move mature modules to SPA.
- Keep admin/legacy pages on Razor until replaced.

## Guardrails
- No breaking auth flow without migration plan.
- Preserve SEO-critical public pages until parity is reached.
- Define deprecation windows per module.
