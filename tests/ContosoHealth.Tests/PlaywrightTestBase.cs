using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using System.Diagnostics;
using System.Net.Sockets;

namespace ContosoHealth.Tests;

public abstract class PlaywrightTestBase : IAsyncLifetime
{
    protected IPlaywright Playwright { get; private set; } = null!;
    protected IBrowser Browser { get; private set; } = null!;
    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;
    protected string BaseUrl { get; private set; } = null!;
    private Process? _appProcess;
    private int _port;

    public async Task InitializeAsync()
    {
        // Find the project path by searching up from test assembly location
        var projectPath = FindProjectPath();
        
        // Get a random available port
        _port = GetAvailablePort();
        BaseUrl = $"http://localhost:{_port}";

        // Build the application first
        await BuildApplicationAsync(projectPath);

        // Start the application
        _appProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"run --no-build --urls {BaseUrl}",
                WorkingDirectory = projectPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                EnvironmentVariables =
                {
                    ["ASPNETCORE_ENVIRONMENT"] = "Development"
                }
            }
        };

        _appProcess.Start();

        // Wait for the application to be ready with health check
        await WaitForApplicationStartAsync();

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

    private static string FindProjectPath()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        
        // Search up the directory tree for the src folder
        while (directory != null)
        {
            var srcPath = Path.Combine(directory.FullName, "src");
            if (Directory.Exists(srcPath))
            {
                return srcPath;
            }
            directory = directory.Parent;
        }
        
        throw new InvalidOperationException("Could not find src directory");
    }

    private static async Task BuildApplicationAsync(string projectPath)
    {
        var buildProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "build --configuration Debug",
                WorkingDirectory = projectPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        buildProcess.Start();
        await buildProcess.WaitForExitAsync();

        if (buildProcess.ExitCode != 0)
        {
            throw new InvalidOperationException("Failed to build application");
        }
    }

    private async Task WaitForApplicationStartAsync()
    {
        var maxAttempts = 30; // 30 seconds max
        var attempt = 0;

        using var client = new HttpClient();
        
        while (attempt < maxAttempts)
        {
            try
            {
                var response = await client.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Redirect)
                {
                    return; // Application is ready
                }
            }
            catch
            {
                // Application not ready yet
            }

            await Task.Delay(1000);
            attempt++;
        }

        throw new TimeoutException("Application failed to start within 30 seconds");
    }

    private static int GetAvailablePort()
    {
        var listener = new TcpListener(System.Net.IPAddress.Loopback, 0);
        listener.Start();
        var port = ((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
}
