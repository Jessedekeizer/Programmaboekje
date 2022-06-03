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
    
    public string Text { get; set; }


    public IActionResult OnGet([FromQuery] int Blok_id)
    {
        Bloks = new BlokRepository().GetAll();
        if (Blok_id != null)
            Text = new BlokRepository().Text(Blok_id);
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
    
    public IActionResult OnPostUpdate(int blok_id)
    {
        BlokRepository BlokCommand = new BlokRepository();
        ;
        return RedirectToPage(new{Blok_id = blok_id});
    }
    
    public IActionResult OnPostSave()
    {
        string note = Request.Form["Text1"];
        BlokRepository BlokCommand = new BlokRepository();
        BlokCommand.AddBlok(1, TimeSpan.Zero, 't', 8, note);
        return RedirectToPage();
    }
}