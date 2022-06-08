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
    
    public string Time { get; set; }

    public int Blok_id { get; set; } = 0;
    
    public string active_pauze { get; set; }
    public string active_orkest { get; set; }
    public string active_tekstvak { get; set; }
    


    public IActionResult OnGet([FromQuery] int blok_id)
    {
        Blok_id = blok_id;
        Bloks = new BlokRepository().GetAll();
        if (Blok_id != null)
            
        foreach (var blok in Bloks)
        {
            if (blok.Blok_id == Blok_id)
            {
                switch (blok.Blok_type)
                {
                    case 'p':
                        active_pauze = "show active";
                        Time = blok.Begintijd.ToString(@"hh\:mm");
                        break;
                      case 't':
                          active_tekstvak = "show active";
                        Text = blok.Tekstvak;
                          break;
                      case 'o':
                          active_orkest = "show active";
                          break;
                }
            }
                
        }
        
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
        
        return RedirectToPage(new{Blok_id = blok_id});
    }
    
    public IActionResult OnPostSave()
    {
        string note = Request.Form["Text1"];
        BlokRepository BlokCommand = new BlokRepository();
        BlokCommand.AddBlok(Blok.Blok_id, 1, Blok.Begintijd , Blok.Blok_type, note);
        return RedirectToPage();
    }
}