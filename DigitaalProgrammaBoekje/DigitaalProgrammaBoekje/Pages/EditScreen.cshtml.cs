using System.Net.Mime;
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
    
    public string OrkestNaam { get; set; }
    
    public int Divisie { get; set; }
    
    public string Time { get; set; }

    public int Blok_id { get; set; } = 0;

    public int Orkest_id { get; set; } = 0;
    
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
                          Time = blok.Begintijd.ToString(@"hh\:mm");
                          OrkestNaam = blok.Orkestgroep.Orkestnaam;
                          Divisie = blok.Orkestgroep.divisie;
                          Orkest_id = blok.Orkestgroep.Orkest_id;
                          Text = blok.Orkestgroep.Muziekstukken;
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
        BlokRepository blokCommand = new BlokRepository();
        if (Blok.Blok_type != 'o')
        {
            blokCommand.AddBlok(Blok.Blok_id, 1, Blok.Begintijd , Blok.Blok_type, note);
        }
        else
        {
            note = Request.Form["Text2"];
            new OrkestgroepRepository().AddOrkestgroep(note, Blok.Orkestgroep.Orkestnaam, 
                Blok.Orkestgroep.divisie, Blok.Orkestgroep.Cijfer, Blok.Orkestgroep.Orkest_id,
                Blok.Blok_id, 1, Blok.Begintijd , Blok.Blok_type);
        }
        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int blok_id, int bloknummer)
    {
        OrkestgroepRepository OrkestCommand = new OrkestgroepRepository();
        int Orkest_id = OrkestCommand.CheckIfOrkestgroep(blok_id);
        if (Orkest_id == 0)
        {
            new BlokRepository().DeleteBlok(blok_id);
        }
        else
        {
            OrkestCommand.DeleteOrkestgroep(Orkest_id, blok_id, bloknummer);
        }
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateCijfer(int Orkest_id, int Number)
    {
        new OrkestgroepRepository().UpdateCijfer(Orkest_id, Number);
        
        return RedirectToPage();
    }
}