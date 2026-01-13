# Contoso Health Playwright Tests

This directory contains end-to-end tests for the Contoso Health application using Playwright for .NET.

## Prerequisites

- .NET 9.0 SDK
- Playwright browsers (automatically installed)

## Setup

1. **Install Playwright browsers:**
   ```bash
   cd tests/ContosoHealth.Tests
   playwright install chromium
   ```

2. **Build the test project:**
   ```bash
   dotnet build
   ```

## Running Tests

### Run all tests:
```bash
dotnet test
```

### Run specific test class:
```bash
dotnet test --filter "FullyQualifiedName~AuthenticationTests"
dotnet test --filter "FullyQualifiedName~HomePageTests"
dotnet test --filter "FullyQualifiedName~DoctorTests"
dotnet test --filter "FullyQualifiedName~PatientTests"
```

### Run a specific test:
```bash
dotnet test --filter "FullyQualifiedName~AuthenticationTests.Login_WithValidCredentials_RedirectsToHomePage"
```

### Run tests with detailed output:
```bash
dotnet test --logger "console;verbosity=detailed"
```

### Run tests in headless mode (default):
Tests are configured to run in headless mode by default for CI/CD compatibility.

## Test Structure

The test project is organized as follows:

```
ContosoHealth.Tests/
├── PlaywrightTestBase.cs           # Base class for all Playwright tests
├── AuthenticationTests.cs          # Tests for login/logout functionality
├── HomePageTests.cs                # Tests for home page
├── DoctorTests.cs                  # Tests for doctor management pages
├── PatientTests.cs                 # Tests for patient management pages  
└── NavigationAndOtherPagesTests.cs # Tests for navigation and other pages
```

## Test Coverage

### Authentication Tests
- Login page display and functionality
- Valid and invalid credential handling
- Logout functionality
- Protected route access control

### Home Page Tests
- Page layout and sections
- Services and specialties display
- Statistics section
- Call-to-action buttons
- Navigation

### Doctor Tests
- Doctor list display
- Doctor detail page
- Image alt text verification (accessibility)
- Navigation from home page

### Patient Tests (Protected)
- Authentication requirement
- Patient list display
- Patient detail page
- Image alt text verification (accessibility)
- Navigation

### Navigation & Other Pages
- Privacy page
- Navigation links
- Error handling
- Responsive design
- HTML structure validation

## Configuration

Tests use `WebApplicationFactory` to create an in-memory test server. The configuration is handled in `PlaywrightTestBase.cs`.

### Browser Configuration
- **Browser**: Chromium
- **Mode**: Headless
- **Viewport**: 1920x1080
- **Options**: --disable-dev-shm-usage (for CI/CD stability)

## CI/CD Integration

To integrate these tests into your CI/CD pipeline:

### GitHub Actions Example
```yaml
- name: Setup .NET
  uses: actions/setup-dotnet@v3
  with:
    dotnet-version: '9.0.x'

- name: Install Playwright browsers
  run: |
    cd tests/ContosoHealth.Tests
    playwright install chromium

- name: Run tests
  run: dotnet test --logger trx --results-directory test-results

- name: Publish test results
  uses: actions/upload-artifact@v3
  if: always()
  with:
    name: test-results
    path: test-results
```

### Azure DevOps Example
```yaml
- task: DotNetCoreCLI@2
  displayName: 'Install Playwright'
  inputs:
    command: 'custom'
    custom: 'tool'
    arguments: 'install --global Microsoft.Playwright.CLI'

- script: |
    playwright install chromium
  displayName: 'Install Playwright browsers'

- task: DotNetCoreCLI@2
  displayName: 'Run Tests'
  inputs:
    command: 'test'
    projects: 'tests/**/*.csproj'
    arguments: '--logger trx'
```

## Troubleshooting

### Playwright browsers not installed
If you see errors about missing browsers:
```bash
cd tests/ContosoHealth.Tests
playwright install
```

### Connection refused errors
Ensure the test server is starting correctly. Check for port conflicts or firewall issues.

### Database errors
The tests use an in-memory SQLite database that's created fresh for each test run.

## Writing New Tests

To add new tests:

1. Create a new test class that inherits from `PlaywrightTestBase`
2. Use the `Page` property to interact with the browser
3. Use Playwright assertions for validation
4. Use the `LoginAsDoctorAsync()` helper method for tests that require authentication

Example:
```csharp
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace ContosoHealth.Tests;

public class MyNewTests : PlaywrightTestBase
{
    [Fact]
    public async Task MyTest_ShouldPass()
    {
        await Page.GotoAsync("/my-page");
        await Expect(Page.Locator("h1")).ToHaveTextAsync("My Page Title");
    }
}
```

## Best Practices

1. **Use data attributes or stable selectors** for locating elements
2. **Wait for elements** before interacting with them
3. **Keep tests independent** - each test should be able to run in isolation
4. **Use descriptive test names** that explain what is being tested
5. **Test user scenarios** rather than implementation details
6. **Handle async operations properly** with appropriate waits

## Resources

- [Playwright for .NET Documentation](https://playwright.dev/dotnet/)
- [xUnit Documentation](https://xunit.net/)
- [ASP.NET Core Testing](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests)
