using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using contosohealth.Data;
using System.Security.Claims;

public class LoginModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public LoginModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Get all doctors from database
        var doctors = await _context.Doctors.ToListAsync();
        
        // Find doctor by matching username format: first 2 chars of first name + first 6 chars of last name (no spaces)
        Doctor? doctor = null;
        foreach (var d in doctors)
        {
            // Remove "Dr. " or "Dr." prefix from first name if present
            var firstName = d.FirstName.Replace("Dr. ", "").Replace("Dr.", "").Trim();
            
            var firstNamePart = firstName.Length >= 2 ? firstName.Substring(0, 2).ToLower() : firstName.ToLower();
            var lastNameNoSpaces = d.LastName.Replace(" ", "");
            var lastNamePart = lastNameNoSpaces.Length >= 6 ? lastNameNoSpaces.Substring(0, 6).ToLower() : lastNameNoSpaces.ToLower();
            var expectedUsername = firstNamePart + lastNamePart;
            
            if (expectedUsername == Username.ToLower())
            {
                doctor = d;
                break;
            }
        }

        // Validate password and doctor existence
        if (doctor == null)
        {
            ErrorMessage = "Invalid username. Username format: first 2 letters of first name + first 6 letters of last name (no spaces).";
            return Page();
        }

        if (Password != "Doctor123!")
        {
            ErrorMessage = "Invalid password. Please try again.";
            return Page();
        }

        // Create claims for the authenticated user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{doctor.FirstName} {doctor.LastName}"),
            new Claim(ClaimTypes.Email, doctor.Email),
            new Claim("DoctorId", doctor.Id.ToString()),
            new Claim(ClaimTypes.Role, "Doctor")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToPage("/Patients");
    }
}
