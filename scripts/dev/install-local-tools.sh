#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
TOOLS_DIR="$REPO_ROOT/.tools"
DOTNET_DIR="$TOOLS_DIR/dotnet"
BIN_DIR="$TOOLS_DIR/bin"

mkdir -p "$DOTNET_DIR" "$BIN_DIR"

curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
bash /tmp/dotnet-install.sh --channel 7.0 --install-dir "$DOTNET_DIR"

curl -fsSL https://github.com/cli/cli/releases/download/v2.65.0/gh_2.65.0_linux_amd64.tar.gz -o /tmp/gh.tar.gz
mkdir -p /tmp/gh-extract
tar -xzf /tmp/gh.tar.gz -C /tmp/gh-extract
cp /tmp/gh-extract/gh_2.65.0_linux_amd64/bin/gh "$BIN_DIR/gh"
chmod +x "$BIN_DIR/gh"

echo "Installed dotnet and gh in $TOOLS_DIR"
echo "Run: source scripts/dev/activate-local-tools.sh"
