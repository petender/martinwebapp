using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using contosohealth.Data;

namespace contosohealth.Pages;

[Authorize]
public class MedicationsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public List<Medication> Medications { get; set; } = new List<Medication>();

    public MedicationsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Medications = await _context.Medications.OrderBy(m => m.Name).ToListAsync();
    }
}
