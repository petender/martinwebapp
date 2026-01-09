using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using martinwebapp.Data;

namespace martinwebapp.Pages;

public class DoctorsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public List<Doctor> Doctors { get; set; } = new List<Doctor>();

    public DoctorsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Doctors = await _context.Doctors.ToListAsync();
    }
}
