using System.ComponentModel.DataAnnotations;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Identity;
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
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        bool chkUser = new GebruikerRepository().checkUsername(RegisterCredential.Username);
        bool chkEmail = new GebruikerRepository().checkEmail(RegisterCredential.Email);
        if (!chkUser && !chkEmail)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, RegisterCredential.Wachtwoord);
            new GebruikerRepository().AddUser(RegisterCredential.Username, RegisterCredential.Email, hashedPassword, 'u', RegisterCredential.Telefoonnummer, RegisterCredential.Dirigent, RegisterCredential.LedenAantal);
            return RedirectToPage("/Login");
        }
        if (chkUser)
            return RedirectToPage(new {warning = 1});
        
        if (chkEmail)
            return RedirectToPage(new {warning = 2});
        
        return RedirectToPage();
    }
}

    
    public class LoginCredentials
    {
        [Required]
        [Display(Name ="Orkest Naam")]
        public string Username { get; set; }
         
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public int Telefoonnummer { get; set; }
        
        [Required]
        public string Dirigent { get; set; }
        
        [Required]
        public char Divisie { get; set; }
        
        public int LedenAantal { get; set; }
    }
