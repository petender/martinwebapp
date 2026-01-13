using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace ContosoHealth.Tests;

public class AuthenticationTests : PlaywrightTestBase
{
    [Fact]
    public async Task LoginPage_DisplaysCorrectly()
    {
        // Navigate to login page
        await Page.GotoAsync("/Login");

        // Check page title
        await Expect(Page).ToHaveTitleAsync("Login - Contoso Clinic");

        // Check main heading
        var heading = Page.Locator("h1");
        await Expect(heading).ToHaveTextAsync("CONTOSO CLINIC");

        // Check username input exists
        var usernameInput = Page.Locator("#Username");
        await Expect(usernameInput).ToBeVisibleAsync();

        // Check password input exists
        var passwordInput = Page.Locator("#Password");
        await Expect(passwordInput).ToBeVisibleAsync();

        // Check login button exists
        var loginButton = Page.Locator("button[type='submit']");
        await Expect(loginButton).ToBeVisibleAsync();
        await Expect(loginButton).ToHaveTextAsync("Sign In");
    }

    [Fact]
    public async Task Login_WithValidCredentials_RedirectsToHomePage()
    {
        // Navigate to login page
        await Page.GotoAsync("/Login");

        // Fill in credentials
        await Page.FillAsync("#Username", "machen");
        await Page.FillAsync("#Password", "Doctor123!");

        // Click login button
        await Page.ClickAsync("button[type='submit']");

        // Wait for navigation to home page
        await Page.WaitForURLAsync("**/");

        // Check that we're on the home page
        await Expect(Page).ToHaveTitleAsync("Contoso Clinic - Your Healthcare Partner");

        // Verify navigation bar shows logout option (user is authenticated)
        var logoutLink = Page.Locator("a[href='/Logout']");
        await Expect(logoutLink).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShowsErrorMessage()
    {
        // Navigate to login page
        await Page.GotoAsync("/Login");

        // Fill in invalid credentials
        await Page.FillAsync("#Username", "invaliduser");
        await Page.FillAsync("#Password", "wrongpassword");

        // Click login button
        await Page.ClickAsync("button[type='submit']");

        // Wait for page to reload
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Check for error message
        var errorAlert = Page.Locator(".alert-danger");
        await Expect(errorAlert).ToBeVisibleAsync();
        await Expect(errorAlert).ToContainTextAsync("Login Failed");
    }

    [Fact]
    public async Task Logout_ClearsAuthenticationAndRedirectsToLogin()
    {
        // First login
        await LoginAsDoctorAsync();

        // Navigate to logout
        await Page.GotoAsync("/Logout");

        // Should redirect to login page
        await Page.WaitForURLAsync("**/Login");

        // Verify we're on login page
        await Expect(Page).ToHaveTitleAsync("Login - Contoso Clinic");
    }

    [Fact]
    public async Task AccessProtectedPage_WithoutAuthentication_RedirectsToLogin()
    {
        // Try to access patients page without logging in
        await Page.GotoAsync("/Patients");

        // Should redirect to login page
        await Page.WaitForURLAsync("**/Login**");

        // Verify we're on login page
        await Expect(Page).ToHaveTitleAsync("Login - Contoso Clinic");
    }

    [Fact]
    public async Task LoginPage_BackToHomeLink_WorksCorrectly()
    {
        // Navigate to login page
        await Page.GotoAsync("/Login");

        // Click back to home link
        var backLink = Page.Locator("a[href='/']");
        await Expect(backLink).ToBeVisibleAsync();
        await backLink.ClickAsync();

        // Should be on home page
        await Page.WaitForURLAsync("**/");
        await Expect(Page).ToHaveTitleAsync("Contoso Clinic - Your Healthcare Partner");
    }
}
