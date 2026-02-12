# Release Policy

## Lifecycle
1. Create `release/<semver>` from `develop`.
2. Run full validation and package release candidate artifact.
3. Auto-deploy to staging.
4. Execute smoke and business checks.
5. Promote to production with manual approval.

## Production Gate
- Manual approval in GitHub Environment `production`.
- Required evidence:
  - PR test evidence
  - rollback steps
  - migration impact note (if DB changed)

## Hotfix Lifecycle
1. Branch `hotfix/*` from `main`.
2. Apply minimal fix.
3. Run required checks.
4. Deploy production via manual approval.
5. Back-merge to `develop`.
