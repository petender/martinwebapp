using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using contosohealth.Data;

namespace contosohealth.Pages;

[Authorize]
public class PatientsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public List<Patient> Patients { get; set; } = new List<Patient>();

    public PatientsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Patients = await _context.Patients.ToListAsync();
    }
}
