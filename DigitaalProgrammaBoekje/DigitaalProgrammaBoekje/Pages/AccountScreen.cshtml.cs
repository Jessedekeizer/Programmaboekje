using DigitaalProgrammaBoekje.Helpers;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class AccountScreen : PageModel
{
    public int Warning { get; set; }
    public string Gebruikersnaam { get; set; }
    public bool is_organisator { get; set; }
    public IEnumerable<Gebruiker> Gebruikers { get; set; }
    
    public IActionResult OnGet(int warning)
    {
        string Logged_in = HttpContext.Session.GetString(SessionConstant.Gebruiker_ID);
        if (Logged_in == null)
            return RedirectToPage("/Login");
        if (new Rolechecker(HttpContext.Session).checkAdmin())
            return RedirectToPage("/AccountScreenAdmin");
        is_organisator = new Rolechecker(HttpContext.Session).checkOrganisator();
        
        Warning = warning;
        Gebruikers = new GebruikerRepository().GetUser(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)));
        return Page();
    }
    
    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Remove(SessionConstant.Gebruiker_ID);
        return RedirectToPage("/Login");
    }

    public IActionResult OnPostUpdateMail([FromForm] string EmailUpd)
    {
        new GebruikerRepository().UpdateEmail(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), EmailUpd);

        return RedirectToPage();
    }
    
    public IActionResult OnPostUpdateUsern([FromForm] string UsernUpdate)
    {
        Warning = new GebruikerRepository().UpdateUsername(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), UsernUpdate );

        return RedirectToPage(new{warning = Warning});
    }
    
    public IActionResult OnPostUpdatePassw([FromForm] string PasswordUpd, [FromForm] string PasswordCurrent)
    {
        GebruikerRepository gebruiker = new GebruikerRepository();
        var hashedPassword = gebruiker.GetPassword(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)));
        
        var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, hashedPassword, PasswordCurrent);
        switch (passwordVerificationResult)
        {
            case PasswordVerificationResult.Failed:
                Warning = 1;
                break;
    
            case PasswordVerificationResult.Success:
                hashedPassword = new PasswordHasher<object?>().HashPassword(null, PasswordUpd);
                gebruiker.UpdatePassword(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), hashedPassword);
            break;
            case PasswordVerificationResult.SuccessRehashNeeded:
                hashedPassword = new PasswordHasher<object?>().HashPassword(null, PasswordUpd);
                gebruiker.UpdatePassword(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), hashedPassword);
            break;
        }
        
        return RedirectToPage(new{warning = Warning});
    }

    public IActionResult OnPostUpdateDirigent([FromForm] string Dirigentupd)
    {
        new GebruikerRepository().UpdateDirigent(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), Dirigentupd);

        return RedirectToPage();
    }
    
    public IActionResult OnPostUpdateNummer([FromForm] string NummerUpd)
    {
        new GebruikerRepository().UpdateNummer(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), NummerUpd);

        return RedirectToPage();
    }

    public IActionResult OnPostUpdateLeden([FromForm] string LedenUpd)
    {
        new OrkestgroepRepository().UpdateAantalLeden(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), LedenUpd);

        return RedirectToPage();
    }
}