using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using martinwebapp.Data;

namespace martinwebapp.Pages;

public class DoctorDetailModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public Doctor? Doctor { get; set; }

    public DoctorDetailModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Doctor = await _context.Doctors.FindAsync(id);
        
        if (Doctor == null)
        {
            return NotFound();
        }

        return Page();
    }
}
