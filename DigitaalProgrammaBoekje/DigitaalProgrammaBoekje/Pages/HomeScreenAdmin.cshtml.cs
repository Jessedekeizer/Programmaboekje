using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class HomeScreenAdmin : PageModel
{
    public IEnumerable<Festival> Festivallist { get; set; }
    public IActionResult OnGet()    
    {
        Festivallist = new FestivalRepository().FestivalsGet();
        return Page();
    }
    
}