#!/usr/bin/env bash
set -euo pipefail

if ! command -v gh >/dev/null 2>&1; then
  echo "gh CLI not found. Install with scripts/dev/install-local-tools.sh"
  exit 1
fi

if ! gh auth status >/dev/null 2>&1; then
  echo "Not authenticated. Run: gh auth login"
  exit 1
fi

repo_arg="${1:-}"
if [[ -n "$repo_arg" ]]; then
  REPO="$repo_arg"
else
  origin_url="$(git remote get-url origin)"
  REPO="$(echo "$origin_url" | sed -E 's#(git@github.com:|https://github.com/)##; s#\.git$##')"
fi

echo "Configuring repository: $REPO"

perm="$(gh repo view "$REPO" --json viewerPermission --jq '.viewerPermission')"
if [[ "$perm" != "ADMIN" && "$perm" != "WRITE" && "$perm" != "MAINTAIN" ]]; then
  echo "Insufficient repository permission: $perm"
  echo "Use an account with WRITE/MAINTAIN/ADMIN access."
  exit 1
fi

labels=(
  "area:backend::0e8a16"
  "area:frontend::1d76db"
  "area:db::5319e7"
  "risk:low::2da44e"
  "risk:medium::fbca04"
  "risk:high::d1242f"
  "needs-migration::5319e7"
  "breaking-change::b60205"
)

for item in "${labels[@]}"; do
  name="${item%%::*}"
  color="${item##*::}"
  gh label create "$name" --color "$color" --repo "$REPO" --force >/dev/null
  echo "Label ensured: $name"
done

target_branches=(develop main)
existing_branches="$(gh api /repos/$REPO/branches --paginate --jq '.[].name')"

if ! echo "$existing_branches" | rg -q '^main$' && ! echo "$existing_branches" | rg -q '^develop$'; then
  if echo "$existing_branches" | rg -q '^master$'; then
    target_branches=(master)
  fi
fi

for branch in "${target_branches[@]}"; do
  if ! echo "$existing_branches" | rg -q "^${branch}$"; then
    echo "Skipping missing branch: $branch"
    continue
  fi

  protection_payload='{
    "required_status_checks": {
      "strict": true,
      "contexts": [
        "Build, Format, Test",
        "SQL Server Migration Validation",
        "Dependency Review",
        "MVC Smoke Test"
      ]
    },
    "enforce_admins": true,
    "required_pull_request_reviews": {
      "dismiss_stale_reviews": true,
      "require_code_owner_reviews": true,
      "required_approving_review_count": 1
    },
    "restrictions": null,
    "required_linear_history": true,
    "allow_force_pushes": false,
    "allow_deletions": false,
    "block_creations": false,
    "required_conversation_resolution": true,
    "lock_branch": false
  }'

  gh api --method PUT \
    -H "Accept: application/vnd.github+json" \
    "/repos/$REPO/branches/$branch/protection" \
    --input - >/dev/null <<< "$protection_payload"
  echo "Branch protection applied: $branch"
done

echo "Done. Now configure GitHub Environments 'staging' and 'production' with required reviewers for production."
