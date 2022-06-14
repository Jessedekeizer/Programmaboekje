using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class Update_jury : PageModel
{
    public IEnumerable<Jurylid> Jurylids { get; set; }
    
    public void OnGet([FromQuery] int jury_id)
    {
        Jurylids = new JurylidRepository().Get1Jury(jury_id);
    }
}