using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class EditScreen : PageModel
{
    public IEnumerable<Blok> Bloks { get; set; }
    public IEnumerable<Orkestgroep> Orkestgroeps { get; set; }
    

    public IActionResult OnGet()
    {
        Bloks = new BlokRepository().hoi();
        Orkestgroeps = new OrkestgroepRepository().Get(1);
        return Page();
    }
}