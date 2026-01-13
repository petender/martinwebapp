using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using System.Diagnostics;

namespace ContosoHealth.Tests;

public abstract class PlaywrightTestBase : IAsyncLifetime
{
    protected IPlaywright Playwright { get; private set; } = null!;
    protected IBrowser Browser { get; private set; } = null!;
    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;
    protected string BaseUrl => "http://localhost:5258"; // Default app port
    private Process? _appProcess;

    public async Task InitializeAsync()
    {
        // Start the application
        var projectPath = Path.GetFullPath(Path.Combine(
            Directory.GetCurrentDirectory(), 
            "..", "..", "..", "..", "..", "src"));
            
        _appProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "run --no-build",
                WorkingDirectory = projectPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        _appProcess.Start();

        // Wait for the application to start
        await Task.Delay(5000); // Give the app time to start

        // Initialize Playwright
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true, // Run in headless mode for CI/CD
            Args = new[] { "--disable-dev-shm-usage" }
        });

        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = BaseUrl,
            IgnoreHTTPSErrors = true,
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        Page = await Context.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        if (Page != null) await Page.CloseAsync();
        if (Context != null) await Context.CloseAsync();
        if (Browser != null) await Browser.CloseAsync();
        Playwright?.Dispose();
        
        // Stop the application
        if (_appProcess != null && !_appProcess.HasExited)
        {
            _appProcess.Kill(true);
            _appProcess.WaitForExit();
            _appProcess.Dispose();
        }
    }

    /// <summary>
    /// Helper method to log in as a doctor
    /// </summary>
    protected async Task LoginAsDoctorAsync(string username = "machen", string password = "Doctor123!")
    {
        await Page.GotoAsync("/Login");
        await Page.FillAsync("#Username", username);
        await Page.FillAsync("#Password", password);
        await Page.ClickAsync("button[type='submit']");
        await Page.WaitForURLAsync("**/");
    }
}
