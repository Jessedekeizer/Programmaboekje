using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class UpdateBedrijf : PageModel
{
    
    public IEnumerable<Bedrijf> Bedrijfs { get; set; }
    
    public IActionResult OnGet([FromQuery] int bedrijf_id)
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
        
        Bedrijfs = new BedrijfRepository().Get1Bedrijf(bedrijf_id);
        return Page();
    }
    
    public IActionResult OnPostUpdateNaam([FromForm] string NaamUpd, [FromForm] int bedrijf_id)
    {
        Bedrijfs = new BedrijfRepository().UpdateNaamBedrijf(bedrijf_id, NaamUpd);

        return RedirectToPage(new {Bedrijf_id = bedrijf_id});
    }

    public IActionResult OnPostUpdateLink([FromForm] string LinkUpd, [FromForm] int bedrijf_id)
    {
        Bedrijfs = new BedrijfRepository().UpdateLinkBedrijf(bedrijf_id, LinkUpd);

        return RedirectToPage(new {Bedrijf_id = bedrijf_id});
    }
    
    public IActionResult OnPostTerug()
    {
        return RedirectToPage("/Bedrijven");
    }
}