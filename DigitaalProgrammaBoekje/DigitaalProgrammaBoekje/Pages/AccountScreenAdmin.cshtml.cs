using DigitaalProgrammaBoekje.Helpers;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigitaalProgrammaBoekje.Pages;

public class AccountScreenAdmin : PageModel
{
    [BindProperty] 
    public LoginCredentials RegisterCredential { get; set; }

    public string Warning2 { get; set; }

    public int Warning { get; set; }
    public string Gebruikersnaam { get; set; }
    public IEnumerable<Gebruiker> Gebruikers { get; set; }
    public IEnumerable<Gebruiker> Organisators { get; set; }
    
    public IActionResult OnGet(int warning, string warning2)
    {
        string Logged_in = HttpContext.Session.GetString(SessionConstant.Gebruiker_ID);
        if (Logged_in == null)
            return RedirectToPage("/Login");
        if (!new Rolechecker(HttpContext.Session).checkAdmin())
            return RedirectToPage("/AccountScreen");
        Warning = warning;
        Warning2 = warning2;
        Gebruikers = new GebruikerRepository().GetUser(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)));
        Organisators = new GebruikerRepository().GetOrganisators();
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
    
    
    public IActionResult OnPostUpdateNummer([FromForm] string NummerUpd)
    {
        new GebruikerRepository().UpdateNummer(Int32.Parse(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), NummerUpd);

        return RedirectToPage();
    }

    public IActionResult OnPostCreateOrganisator()
    {
        bool chkUser = new GebruikerRepository().checkUsername(RegisterCredential.Username);
        if (!chkUser)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, RegisterCredential.Wachtwoord);
            new GebruikerRepository().AddOrganisator(RegisterCredential.Username, hashedPassword);
            return RedirectToPage();
        }
        if (chkUser)
            return RedirectToPage(new {warning2 = "Gebruikersnaam Bestaat al"});
        return RedirectToPage();
    }

    public IActionResult OnPostDeleteOrganisator([FromForm] int gebruikerid)
    {
        new GebruikerRepository().DeleteOrganisator(gebruikerid);
        return RedirectToPage();
    }
    
    public class LoginCredentials
    {
        [Required]
        [Display(Name ="Gebruikers Naam")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
        
    }
}