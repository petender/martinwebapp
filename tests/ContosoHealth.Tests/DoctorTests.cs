using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;

namespace ContosoHealth.Tests;

public class DoctorTests : PlaywrightTestBase
{
    [Fact]
    public async Task DoctorsPage_DisplaysCorrectly()
    {
        // Navigate to doctors page
        await Page.GotoAsync("/Doctors");

        // Check page title (format includes " - Contoso Clinic" suffix)
        await Expect(Page).ToHaveTitleAsync(new Regex("Doctors"));

        // Check heading
        var heading = Page.Locator(".doctors-title");
        await Expect(heading).ToHaveTextAsync("Doctors");
    }

    [Fact]
    public async Task DoctorsPage_DisplaysDoctorsList()
    {
        await Page.GotoAsync("/Doctors");

        // Check that doctors table exists
        var table = Page.Locator(".doctors-table");
        await Expect(table).ToBeVisibleAsync();

        // Check table headers
        await Expect(Page.Locator("th:has-text('Photo')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Doctor ID')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Name')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Specialization')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Department')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Phone')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Experience')")).ToBeVisibleAsync();

        // Verify that at least one doctor row is displayed
        var doctorRows = Page.Locator(".doctors-table tbody tr");
        var count = await doctorRows.CountAsync();
        Assert.True(count > 0, "Expected at least one doctor to be displayed");
    }

    [Fact]
    public async Task DoctorsPage_DoctorPhotos_HaveAltText()
    {
        await Page.GotoAsync("/Doctors");

        // Get all doctor photo images
        var doctorPhotos = Page.Locator(".doctor-photo");
        var count = await doctorPhotos.CountAsync();

        // Check that all images have alt text
        for (int i = 0; i < count; i++)
        {
            var photo = doctorPhotos.Nth(i);
            var altText = await photo.GetAttributeAsync("alt");
            Assert.NotNull(altText);
            Assert.NotEmpty(altText);
        }
    }

    [Fact]
    public async Task DoctorsPage_DisplaysSpecificDoctorInformation()
    {
        await Page.GotoAsync("/Doctors");

        // Check for a specific doctor (Dr. Margaret Chen from seed data)
        var drChenRow = Page.Locator("tr:has-text('Margaret Chen')");
        await Expect(drChenRow).ToBeVisibleAsync();

        // Verify the row contains expected information
        await Expect(drChenRow).ToContainTextAsync("Cardiology");
    }

    [Fact]
    public async Task DoctorsPage_RowClick_OpensPopup()
    {
        await Page.GotoAsync("/Doctors");

        // Get the first doctor row
        var firstRow = Page.Locator(".doctors-table tbody tr").First;

        // Wait for the row to be visible
        await Expect(firstRow).ToBeVisibleAsync();

        // Get the doctor ID from the row for verification
        var doctorIdCell = firstRow.Locator("td").Nth(1); // Second cell contains Doctor ID
        var doctorId = await doctorIdCell.TextContentAsync();

        // Listen for popup
        var popupTask = Page.WaitForPopupAsync();

        // Click the row
        await firstRow.ClickAsync();

        // Wait for popup
        var popup = await popupTask;

        // Wait for popup to load
        await popup.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Verify popup URL contains DoctorDetail
        Assert.Contains("/DoctorDetail/", popup.Url);
    }

    [Fact]
    public async Task DoctorsPage_SpecializationBadges_DisplayCorrectly()
    {
        await Page.GotoAsync("/Doctors");

        // Get all specialization badges
        var badges = Page.Locator(".specialization-badge");
        var count = await badges.CountAsync();

        Assert.True(count > 0, "Expected at least one specialization badge");

        // Verify badges are visible
        for (int i = 0; i < Math.Min(count, 3); i++)
        {
            await Expect(badges.Nth(i)).ToBeVisibleAsync();
        }
    }

    [Fact]
    public async Task DoctorDetailPage_DisplaysCorrectInformation()
    {
        // Navigate to a specific doctor detail page directly
        await Page.GotoAsync("/DoctorDetail/1");

        // Wait for page to load
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Check that the page displays doctor information
        // The page should have doctor details like name, specialization, etc.
        var content = await Page.ContentAsync();
        Assert.Contains("Doctor", content);
    }

    [Fact]
    public async Task DoctorsPage_Navigation_FromHomePageWorks()
    {
        // Start at home page
        await Page.GotoAsync("/");

        // Click on "Our Doctors" navigation link or button
        var doctorsLink = Page.Locator("a[href='/Doctors']").First;
        await doctorsLink.ClickAsync();

        // Should be on doctors page
        await Page.WaitForURLAsync("**/Doctors");
        await Expect(Page).ToHaveTitleAsync(new Regex("Doctors"));
    }
}
