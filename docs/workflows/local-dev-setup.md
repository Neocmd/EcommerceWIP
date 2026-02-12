# Local Dev Setup

## Install local toolchain in repo

```bash
bash scripts/dev/install-local-tools.sh
```

## Activate local toolchain

```bash
source scripts/dev/activate-local-tools.sh
```

This configures local-only variables in `.tools/`:
- `PATH` includes local `dotnet` and `gh`
- `DOTNET_CLI_HOME` points to `.tools/home`
- `NUGET_PACKAGES` points to `.tools/nuget`

## Validate project

```bash
dotnet build Ecommerce.sln --configuration Release
dotnet test Ecommerce.sln --configuration Release --no-build
```

## Configure GitHub repo settings via CLI

```bash
source scripts/dev/activate-local-tools.sh
gh auth login
bash scripts/github/bootstrap-repo-settings.sh
```
