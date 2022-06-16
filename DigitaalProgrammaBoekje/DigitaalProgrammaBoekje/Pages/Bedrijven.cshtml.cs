using System.Security.Policy;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class Bedrijven : PageModel
{
    public IEnumerable<Bedrijf> Bedrijfs { get; set; }
    
    public IEnumerable<Jurylid> Jurylids { get; set; }
    
    public Jurylid Jurylid { get; set; }

    public void OnGet()
    {
        Jurylids = new JurylidRepository().GetAllJury();

        Bedrijfs = new BedrijfRepository().GetAllbedrijf();
        
    }

    public IActionResult OnPostJuryUpd([FromForm]int jury_id)
    {
        
        return RedirectToPage("/Update jury", new{Jury_id = jury_id});
    }
    
    public IActionResult OnPostBedrijfUpd([FromForm]int bedrijf_id)
    {
        
        return RedirectToPage("/UpdateBedrijf", new{Bedrijf_id = bedrijf_id});
    }
}