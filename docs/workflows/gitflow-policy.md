# GitFlow Policy

## Branches
- `main`: production-ready history.
- `develop`: integration branch for features.
- `feature/*`: new work; PR target `develop`.
- `release/*`: hardening and release preparation; PR target `main` and back-merge to `develop`.
- `hotfix/*`: urgent fixes from `main`; PR target `main` and back-merge to `develop`.

## Naming
- `feature/<area>-<ticket>`
- `release/<semver>`
- `hotfix/<ticket-or-incident>`

## PR Rules
- At least 1 code owner approval required.
- Required checks must be green.
- Squash merge disabled for release/hotfix PRs (preserve traceability).
- Force-push discouraged after review starts.

## Merge Targets
- Feature PRs: `develop` only.
- Release PRs: `main`, then merge `main` back into `develop`.
- Hotfix PRs: `main`, then merge `main` back into `develop`.
