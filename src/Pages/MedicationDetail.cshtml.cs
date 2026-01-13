using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using contosohealth.Data;

namespace contosohealth.Pages;

[Authorize]
public class MedicationDetailModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public Medication? Medication { get; set; }

    public MedicationDetailModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Medication = await _context.Medications.FindAsync(id);

        if (Medication == null)
        {
            return NotFound();
        }

        return Page();
    }
}
