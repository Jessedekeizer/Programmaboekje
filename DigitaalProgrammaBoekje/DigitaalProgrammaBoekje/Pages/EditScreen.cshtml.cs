using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class EditScreen : PageModel
{
    public IEnumerable<Blok> Bloks { get; set; }
    
    [BindProperty]
    public Blok Blok { get; set; }


    public IActionResult OnGet()
    {
        Bloks = new BlokRepository().GetAll();
        return Page();
    }

    public IActionResult OnPostUp( int blok_id, int bloknummer, int festival_id)
    {
        BlokRepository BlokCommand = new BlokRepository();
        BlokCommand.ChangeRankingUp(bloknummer, blok_id, festival_id);
        return RedirectToPage();
    }
    
    public IActionResult OnPostDown(int blok_id, int bloknummer, int festival_id)
    {
        BlokRepository BlokCommand = new BlokRepository();
        BlokCommand.ChangeRankingDown( bloknummer, blok_id, festival_id);
        return RedirectToPage();
    }
}