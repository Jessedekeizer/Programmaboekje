using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class Programma : PageModel
{
    public IEnumerable<Blok> Bloks { get; set; }

    public IEnumerable<Sponsort> Sponsorts { get; set; }
    
    public IEnumerable<Jurylid> Jurylids { get; set; }

    public void OnGet([FromQuery] int Festival_id)
    {
        if (Festival_id != 0)
        {
            Sponsorts = new SponsortRepository().GetSponsors(Festival_id);

            Jurylids = new JurylidRepository().GetJury(Festival_id);

            Bloks = new BlokRepository().GetAll(Festival_id);
        }
    }
}