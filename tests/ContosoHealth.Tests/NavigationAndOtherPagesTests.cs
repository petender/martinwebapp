using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;

namespace ContosoHealth.Tests;

public class NavigationAndOtherPagesTests : PlaywrightTestBase
{
    [Fact]
    public async Task PrivacyPage_DisplaysCorrectly()
    {
        // Navigate to privacy page
        await Page.GotoAsync("/Privacy");

        // Check page title contains "Privacy"
        var title = await Page.TitleAsync();
        Assert.Contains("Privacy", title);

        // Check that privacy content is displayed
        var content = await Page.ContentAsync();
        Assert.Contains("Privacy", content);
    }

    [Fact]
    public async Task Navigation_HomeLink_WorksFromAllPages()
    {
        // Test from Doctors page
        await Page.GotoAsync("/Doctors");
        var homeLink = Page.Locator("a[href='/']:has-text('Home'), a[href='/']:has-text('Contoso')").First;
        await homeLink.ClickAsync();
        await Page.WaitForURLAsync("**/");
        await Expect(Page).ToHaveTitleAsync(new Regex("Contoso Clinic.*Your Healthcare Partner"));

        // Test from Privacy page
        await Page.GotoAsync("/Privacy");
        homeLink = Page.Locator("a[href='/']:has-text('Home'), a[href='/']:has-text('Contoso')").First;
        await homeLink.ClickAsync();
        await Page.WaitForURLAsync("**/");
        await Expect(Page).ToHaveTitleAsync(new Regex("Contoso Clinic.*Your Healthcare Partner"));
    }

    [Fact]
    public async Task Navigation_PrivacyLink_WorksFromHomePage()
    {
        // Start at home page
        await Page.GotoAsync("/");

        // Find and click Privacy link (typically in footer)
        var privacyLink = Page.Locator("a[href='/Privacy']").First;
        await privacyLink.ClickAsync();

        // Should be on privacy page
        await Page.WaitForURLAsync("**/Privacy");
        var title = await Page.TitleAsync();
        Assert.Contains("Privacy", title);
    }

    [Fact]
    public async Task Navigation_DoctorsLink_AvailableInNavbar()
    {
        await Page.GotoAsync("/");

        // Check that Doctors link exists in navigation
        var doctorsLink = Page.Locator("nav a[href='/Doctors'], .navbar a[href='/Doctors']").First;
        await Expect(doctorsLink).ToBeVisibleAsync();

        // Click it
        await doctorsLink.ClickAsync();
        await Page.WaitForURLAsync("**/Doctors");
        await Expect(Page).ToHaveTitleAsync(new Regex("Doctors"));
    }

    [Fact]
    public async Task Navigation_LoginLink_AvailableWhenNotAuthenticated()
    {
        await Page.GotoAsync("/");

        // Check that Login link exists
        var loginLink = Page.Locator("a[href='/Login']");
        await Expect(loginLink).ToBeVisibleAsync();

        // Click it
        await loginLink.ClickAsync();
        await Page.WaitForURLAsync("**/Login");
        await Expect(Page).ToHaveTitleAsync("Login - Contoso Clinic");
    }

    [Fact]
    public async Task Navigation_LogoutLink_AvailableWhenAuthenticated()
    {
        // Login first
        await LoginAsDoctorAsync();

        // Check that Logout link exists
        var logoutLink = Page.Locator("a[href='/Logout']");
        await Expect(logoutLink).ToBeVisibleAsync();

        // Verify Login link is not visible when authenticated
        var loginLink = Page.Locator("a[href='/Login']");
        var loginCount = await loginLink.CountAsync();
        Assert.Equal(0, loginCount);
    }

    [Fact]
    public async Task Navigation_PatientsLink_AvailableWhenAuthenticated()
    {
        // Login first
        await LoginAsDoctorAsync();

        // Check that Patients link exists in navigation
        var patientsLink = Page.Locator("nav a[href='/Patients'], .navbar a[href='/Patients']").First;
        await Expect(patientsLink).ToBeVisibleAsync();

        // Click it
        await patientsLink.ClickAsync();
        await Page.WaitForURLAsync("**/Patients");
        await Expect(Page).ToHaveTitleAsync(new Regex("Patients"));
    }

    [Fact]
    public async Task ErrorPage_DisplaysWhenNavigatingToInvalidPage()
    {
        // Navigate to a non-existent page
        var response = await Page.GotoAsync("/NonExistentPage123");

        // Should get 404 response
        Assert.NotNull(response);
        Assert.Equal(404, response.Status);
    }

    [Fact]
    public async Task Layout_ContainsNavigationBar()
    {
        await Page.GotoAsync("/");

        // Check that navigation/navbar exists
        var navbar = Page.Locator("nav, .navbar");
        await Expect(navbar.First).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Layout_ResponsiveDesign_WorksOnMobileViewport()
    {
        // Set mobile viewport
        await Page.SetViewportSizeAsync(375, 667);

        await Page.GotoAsync("/");

        // Page should still load and be visible
        await Expect(Page.Locator("body")).ToBeVisibleAsync();

        // Check that main content is visible
        var heroSection = Page.Locator(".hero-section");
        await Expect(heroSection).ToBeVisibleAsync();
    }

    [Fact]
    public async Task AllPages_HaveValidHTMLStructure()
    {
        var pages = new[] { "/", "/Doctors", "/Privacy", "/Login" };

        foreach (var pagePath in pages)
        {
            await Page.GotoAsync(pagePath);

            // Check basic HTML structure
            var html = Page.Locator("html");
            await Expect(html).ToBeVisibleAsync();

            var body = Page.Locator("body");
            await Expect(body).ToBeVisibleAsync();

            // Check that page has a title
            var title = await Page.TitleAsync();
            Assert.NotEmpty(title);
        }
    }
}
