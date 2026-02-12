#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
TOOLS_DIR="$REPO_ROOT/.tools"
DOTNET_DIR="$TOOLS_DIR/dotnet"
BIN_DIR="$TOOLS_DIR/bin"
DOTNET_HOME_DIR="$TOOLS_DIR/home"

mkdir -p "$DOTNET_HOME_DIR" "$BIN_DIR"

export PATH="$DOTNET_DIR:$BIN_DIR:$PATH"
export DOTNET_CLI_HOME="$DOTNET_HOME_DIR"
export NUGET_PACKAGES="$TOOLS_DIR/nuget"
export TMPDIR="$TOOLS_DIR/tmp"
export GH_CONFIG_DIR="${GH_CONFIG_DIR:-$HOME/.config/gh}"

mkdir -p "$NUGET_PACKAGES" "$TMPDIR"

echo "Local toolchain activated"
echo "PATH prefix: $DOTNET_DIR:$BIN_DIR"
