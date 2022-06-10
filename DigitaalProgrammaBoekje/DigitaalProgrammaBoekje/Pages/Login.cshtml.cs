using System.ComponentModel.DataAnnotations;
using DigitaalProgrammaBoekje.Helpers;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class Login : PageModel
{
    [BindProperty] 
    public LoginCredentials LoginCredential { get; set; }

    public string Warning { get; set; }

    public void OnGet([FromQuery] int warning)
    {
        switch (warning)
        {
            case 1:
                Warning = "De gebruikersnaam of het wachtwoord is incorrect.";
                break;
            case 2:
                Warning = "Er is iets misgegaan probeer het later opnieuw of neem contact op.";
                break;
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        int Gebruiker_id;
        GebruikerRepository gebruiker = new GebruikerRepository();
        if (gebruiker.checkUsername(LoginCredential.Orkest))
        {
            Gebruiker_id = gebruiker.Gebruiker_ID(LoginCredential.Orkest);
        }
        else
        {
            return RedirectToPage(new {warning = 1});
        }
        
        var hashedPassword = gebruiker.GetPassword(Gebruiker_id);
        //vergelijkt opgegeven password met het hashed password 
        var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, hashedPassword, LoginCredential.Wachtwoord);
        switch (passwordVerificationResult)
        {
            case PasswordVerificationResult.Failed:
                return RedirectToPage(new {warning = 1});
    
            case PasswordVerificationResult.Success:
                
                HttpContext.Session.SetString(SessionConstant.Gebruiker_ID, Gebruiker_id.ToString());
                return RedirectToPage("/HomeScreenAdmin");
            //Als de hash niet veilig is, maar je kan wel inloggen
            case PasswordVerificationResult.SuccessRehashNeeded:
                HttpContext.Session.SetString(SessionConstant.Gebruiker_ID, Gebruiker_id.ToString());
                return RedirectToPage("/HomeScreen");
        }
        //als geen van de cases wordt uitgevoerd
        return RedirectToPage(new {warning = 2});
    }
    
    public class LoginCredentials
    {
        [Required]
        [Display(Name ="Orkest Naam")]
        public string Orkest { get; set; }
        
        

        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
    }
}