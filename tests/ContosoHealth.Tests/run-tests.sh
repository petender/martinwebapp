#!/bin/bash

# Script to run Playwright tests for Contoso Health application

set -e

echo "========================================="
echo "Contoso Health - Playwright Test Runner"
echo "========================================="
echo ""

# Check if we're in the right directory
if [ ! -f "ContosoHealth.Tests.csproj" ]; then
    echo "Error: This script must be run from the tests/ContosoHealth.Tests directory"
    exit 1
fi

# Check if Playwright is installed
echo "Checking Playwright installation..."
if ! command -v playwright &> /dev/null; then
    echo "Playwright CLI not found. Installing..."
    dotnet tool install --global Microsoft.Playwright.CLI
fi

# Install browsers if needed
echo "Ensuring Playwright browsers are installed..."
if ! playwright install chromium --with-deps 2>&1 | tee /tmp/playwright-install.log; then
    echo "Warning: Playwright browser installation with deps failed, trying without deps..."
    playwright install chromium || echo "Warning: Failed to install Playwright browsers. Tests may fail."
fi

# Build the project
echo ""
echo "Building test project..."
dotnet build

# Run tests based on argument
echo ""
echo "Running tests..."
echo "========================================="

if [ $# -eq 0 ]; then
    # No arguments - run all tests
    echo "Running ALL tests..."
    dotnet test --logger "console;verbosity=normal"
else
    # Run specific test filter
    echo "Running tests matching filter: $1..."
    dotnet test --filter "$1" --logger "console;verbosity=detailed"
fi

echo ""
echo "========================================="
echo "Test run completed!"
echo "========================================="
