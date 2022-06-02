using System.ComponentModel.DataAnnotations;
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