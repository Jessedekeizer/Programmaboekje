using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class Register : PageModel
{
    [BindProperty] 
    public LoginCredentials RegisterCredential { get; set; }

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
        [Display(Name ="User Name")]
        public string Username { get; set; }
         
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}