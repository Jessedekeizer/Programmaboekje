using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;

namespace DigitaalProgrammaBoekje.Pages;

public class TestJuryView : PageModel
{
    public IEnumerable<Jurylid> Jurylist { get; set; }

    public void OnGet()
    {
        JurylidRepository jurylist = new JurylidRepository();
        Jurylist = jurylist.GetJury(1);
    }
}