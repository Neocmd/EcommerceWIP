# GitHub Setup Checklist

## Required Repository Secrets
- `CI_SQL_SA_PASSWORD`
- `AZURE_WEBAPP_NAME`
- `AZURE_WEBAPP_PUBLISH_PROFILE_STAGING`
- `AZURE_WEBAPP_PUBLISH_PROFILE_PROD`

## Environments
- `staging`
- `production` (must require manual reviewers)

## Branch Protection

Apply to `develop` and `main`:
- Require pull request before merging
- Require approvals (minimum 1)
- Require review from Code Owners
- Require status checks to pass
- Dismiss stale approvals on new commits

### Required checks
- `Build, Format, Test`
- `SQL Server Migration Validation`
- `Dependency Review`
- `MVC Smoke Test`

## Labels to Create
- `area:backend`
- `area:frontend`
- `area:db`
- `risk:low`
- `risk:medium`
- `risk:high`
- `needs-migration`
- `breaking-change`

## Optional CLI Bootstrap
After authenticating with GitHub CLI, you can bootstrap labels and branch protections:

```bash
source scripts/dev/activate-local-tools.sh
gh auth login
bash scripts/github/bootstrap-repo-settings.sh
```

Requirements:
- Authenticated `gh` account must have `WRITE`/`MAINTAIN`/`ADMIN` permission on the repo.
- Target branches must exist remotely (`develop`/`main`, or `master` fallback).
