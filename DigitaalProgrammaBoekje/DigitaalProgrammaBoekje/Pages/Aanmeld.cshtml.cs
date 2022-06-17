using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class Aanmeld : PageModel
{
    public IEnumerable<Gebruiker> Gebruikers { get; set; }

    public IActionResult OnGet()
    {
        if (new Rolechecker(HttpContext.Session).Loged_in())
        {
            if (new Rolechecker(HttpContext.Session).checkUser())
            {
                return RedirectToPage("/HomeScreen");
            }
        }
        else
        {
            return  RedirectToPage("/Login");
        }

        Gebruikers = new Meld_aanRepository().ShowFestivalMeld_aan(Convert.ToInt16(Request.Cookies["Festival_id"]));
        
        return Page();
    }
}