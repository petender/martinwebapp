using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace ContosoHealth.Tests;

public class PatientTests : PlaywrightTestBase
{
    [Fact]
    public async Task PatientsPage_RequiresAuthentication()
    {
        // Try to access patients page without logging in
        await Page.GotoAsync("/Patients");

        // Should redirect to login page
        await Page.WaitForURLAsync("**/Login**");
        await Expect(Page).ToHaveTitleAsync("Login - Contoso Clinic");
    }

    [Fact]
    public async Task PatientsPage_AfterLogin_DisplaysCorrectly()
    {
        // Login first
        await LoginAsDoctorAsync();

        // Navigate to patients page
        await Page.GotoAsync("/Patients");

        // Check page title
        await Expect(Page).ToHaveTitleAsync("Patients");

        // Check heading
        var heading = Page.Locator(".patients-title");
        await Expect(heading).ToHaveTextAsync("Patients");
    }

    [Fact]
    public async Task PatientsPage_DisplaysPatientsList()
    {
        // Login first
        await LoginAsDoctorAsync();

        await Page.GotoAsync("/Patients");

        // Check that patients table exists
        var table = Page.Locator(".patients-table");
        await Expect(table).ToBeVisibleAsync();

        // Check table headers
        await Expect(Page.Locator("th:has-text('Photo')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Patient ID')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Name')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Date of Birth')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Gender')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Phone')")).ToBeVisibleAsync();
        await Expect(Page.Locator("th:has-text('Blood Type')")).ToBeVisibleAsync();

        // Verify that at least one patient row is displayed
        var patientRows = Page.Locator(".patients-table tbody tr");
        var count = await patientRows.CountAsync();
        Assert.True(count > 0, "Expected at least one patient to be displayed");
    }

    [Fact]
    public async Task PatientsPage_PatientPhotos_HaveAltText()
    {
        // Login first
        await LoginAsDoctorAsync();

        await Page.GotoAsync("/Patients");

        // Get all patient photo images
        var patientPhotos = Page.Locator(".patient-photo");
        var count = await patientPhotos.CountAsync();

        // Check that all images have alt text
        for (int i = 0; i < count; i++)
        {
            var photo = patientPhotos.Nth(i);
            var altText = await photo.GetAttributeAsync("alt");
            Assert.NotNull(altText);
            Assert.NotEmpty(altText);
        }
    }

    [Fact]
    public async Task PatientsPage_GenderBadges_DisplayCorrectly()
    {
        // Login first
        await LoginAsDoctorAsync();

        await Page.GotoAsync("/Patients");

        // Get all gender badges
        var badges = Page.Locator(".gender-badge");
        var count = await badges.CountAsync();

        Assert.True(count > 0, "Expected at least one gender badge");

        // Verify badges are visible
        for (int i = 0; i < Math.Min(count, 3); i++)
        {
            await Expect(badges.Nth(i)).ToBeVisibleAsync();
            var badgeText = await badges.Nth(i).TextContentAsync();
            Assert.True(badgeText == "Male" || badgeText == "Female", 
                $"Expected gender badge to be 'Male' or 'Female', but got '{badgeText}'");
        }
    }

    [Fact]
    public async Task PatientsPage_RowClick_OpensPopup()
    {
        // Login first
        await LoginAsDoctorAsync();

        await Page.GotoAsync("/Patients");

        // Get the first patient row
        var firstRow = Page.Locator(".patients-table tbody tr").First;

        // Wait for the row to be visible
        await Expect(firstRow).ToBeVisibleAsync();

        // Listen for popup
        var popupTask = Page.WaitForPopupAsync();

        // Click the row
        await firstRow.ClickAsync();

        // Wait for popup
        var popup = await popupTask;

        // Wait for popup to load
        await popup.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Verify popup URL contains PatientDetail
        Assert.Contains("/PatientDetail/", popup.Url);
    }

    [Fact]
    public async Task PatientsPage_DisplaysPatientInformation()
    {
        // Login first
        await LoginAsDoctorAsync();

        await Page.GotoAsync("/Patients");

        // Get the first patient row
        var firstRow = Page.Locator(".patients-table tbody tr").First;
        await Expect(firstRow).ToBeVisibleAsync();

        // Verify the row contains patient information elements
        var cells = firstRow.Locator("td");
        var cellCount = await cells.CountAsync();
        Assert.True(cellCount >= 7, "Expected at least 7 cells in patient row");

        // Check that photo exists
        var photo = firstRow.Locator(".patient-photo");
        await Expect(photo).ToBeVisibleAsync();

        // Check that name exists
        var name = firstRow.Locator(".patient-name");
        await Expect(name).ToBeVisibleAsync();
    }

    [Fact]
    public async Task PatientDetailPage_RequiresAuthentication()
    {
        // Try to access patient detail page without logging in
        await Page.GotoAsync("/PatientDetail/1");

        // Should redirect to login page
        await Page.WaitForURLAsync("**/Login**");
        await Expect(Page).ToHaveTitleAsync("Login - Contoso Clinic");
    }

    [Fact]
    public async Task PatientDetailPage_AfterLogin_DisplaysCorrectInformation()
    {
        // Login first
        await LoginAsDoctorAsync();

        // Navigate to a specific patient detail page directly
        await Page.GotoAsync("/PatientDetail/1");

        // Wait for page to load
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Check that the page displays patient information
        var content = await Page.ContentAsync();
        Assert.Contains("Patient", content);
    }

    [Fact]
    public async Task PatientsPage_Navigation_FromHomePageWorks()
    {
        // Login first
        await LoginAsDoctorAsync();

        // Go to home page
        await Page.GotoAsync("/");

        // Click on "View Patients" button or link
        var patientsLink = Page.Locator("a[href='/Patients']").First;
        await patientsLink.ClickAsync();

        // Should be on patients page
        await Page.WaitForURLAsync("**/Patients");
        await Expect(Page).ToHaveTitleAsync("Patients");
    }
}
