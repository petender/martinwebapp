using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace contosohealth.Pages;

public class Employee
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string HiringDate { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
}

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    private readonly IWebHostEnvironment _environment;

    public List<Employee> Employees { get; set; } = new List<Employee>();

    public PrivacyModel(ILogger<PrivacyModel> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public void OnGet()
    {
        var jsonPath = Path.Combine(_environment.ContentRootPath, "sampledata.json");
        if (System.IO.File.Exists(jsonPath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            Employees = JsonSerializer.Deserialize<List<Employee>>(jsonData) ?? new List<Employee>();
        }
    }
}

