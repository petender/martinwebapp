using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using martinwebapp.Data;

namespace martinwebapp.Pages;

public class PatientDetailModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public Patient? Patient { get; set; }

    public PatientDetailModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Patient = await _context.Patients.FindAsync(id);
        
        if (Patient == null)
        {
            return NotFound();
        }

        return Page();
    }
}
