using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;

namespace ContosoHealth.Tests;

public class HomePageTests : PlaywrightTestBase
{
    [Fact]
    public async Task HomePage_DisplaysCorrectly()
    {
        // Navigate to home page
        await Page.GotoAsync("/");

        // Check page title (format is: "Title - Contoso Clinic")
        await Expect(Page).ToHaveTitleAsync("Contoso Clinic - Your Healthcare Partner - Contoso Clinic");

        // Check main heading in hero section
        var heroHeading = Page.Locator(".hero-section h1");
        await Expect(heroHeading).ToHaveTextAsync("CONTOSO CLINIC");

        // Check hero subtitle
        var heroSubtitle = Page.Locator(".hero-section p").First;
        await Expect(heroSubtitle).ToContainTextAsync("Excellence in Healthcare Since 1995");
    }

    [Fact]
    public async Task HomePage_ServicesSection_DisplaysAllServices()
    {
        await Page.GotoAsync("/");

        // Check services section title
        var servicesTitle = Page.Locator(".services-section .section-title");
        await Expect(servicesTitle).ToHaveTextAsync("Our Services");

        // Verify all 6 service cards are present
        var serviceCards = Page.Locator(".service-card");
        await Expect(serviceCards).ToHaveCountAsync(6);

        // Check for specific services
        await Expect(Page.Locator("text=Emergency Care")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Diagnostic Services")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Specialized Care")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Primary Care")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Pharmacy Services")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Wellness Programs")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task HomePage_SpecialtiesSection_DisplaysAllSpecialties()
    {
        await Page.GotoAsync("/");

        // Check specialties section title
        var specialtiesTitle = Page.Locator(".specialties-section .section-title");
        await Expect(specialtiesTitle).ToHaveTextAsync("Medical Specialties");

        // Verify all 8 specialty cards are present
        var specialtyCards = Page.Locator(".specialty-card");
        await Expect(specialtyCards).ToHaveCountAsync(8);

        // Check for specific specialties
        await Expect(Page.Locator("text=Cardiology")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Orthopedics")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Pediatrics")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Neurology")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task HomePage_StatisticsSection_DisplaysCorrectStats()
    {
        await Page.GotoAsync("/");

        // Check statistics section
        var statsSection = Page.Locator(".stats-section");
        await Expect(statsSection).ToBeVisibleAsync();

        // Verify statistics
        await Expect(Page.Locator("text=50+")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Expert Doctors")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=15K+")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Patients Served")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=24/7")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Emergency Care")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=98%")).ToBeVisibleAsync();
        await Expect(Page.Locator("text=Patient Satisfaction")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task HomePage_HeroButtons_NavigateCorrectly()
    {
        await Page.GotoAsync("/");

        // Click "View Patients" button (should redirect to login)
        var viewPatientsButton = Page.Locator("a[href='/Patients'].btn-primary-custom");
        await Expect(viewPatientsButton).ToBeVisibleAsync();
        await viewPatientsButton.ClickAsync();
        await Page.WaitForURLAsync("**/Login**");

        // Go back to home
        await Page.GotoAsync("/");

        // Click "Our Doctors" button
        var ourDoctorsButton = Page.Locator("a[href='/Doctors'].btn-secondary-custom");
        await Expect(ourDoctorsButton).ToBeVisibleAsync();
        await ourDoctorsButton.ClickAsync();
        await Page.WaitForURLAsync("**/Doctors");
        await Expect(Page).ToHaveTitleAsync(new Regex("Doctors"));
    }

    [Fact]
    public async Task HomePage_CTASection_DisplaysCorrectly()
    {
        await Page.GotoAsync("/");

        // Check call to action section
        var ctaSection = Page.Locator(".cta-section");
        await Expect(ctaSection).ToBeVisibleAsync();

        var ctaHeading = ctaSection.Locator("h2");
        await Expect(ctaHeading).ToContainTextAsync("Ready to Experience World-Class Healthcare?");

        var ctaButton = ctaSection.Locator("a[href='/Patients']");
        await Expect(ctaButton).ToBeVisibleAsync();
        await Expect(ctaButton).ToHaveTextAsync("Get Started");
    }
}
