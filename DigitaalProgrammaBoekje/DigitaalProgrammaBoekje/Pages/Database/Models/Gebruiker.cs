using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Gebruiker
{
    [Required]
    public int gebruiker_id { get; set; }
    [Required, MinLength(2), MaxLength(128)]
    public string email { get; set; }
    [Required, MinLength(2), MaxLength(128)]
    public string naam { get; set; }
    [Required, MinLength(2), MaxLength(128)]
    public string wachtwoord { get; set; }
    [Required]
    public  char functie { get; set; }
    [Required, MinLength(2), MaxLength(128)]
    public int telefoonnummer { get; set; }
    [Required]
    public string dirigent { get; set; }
    [Required]
    public int Leden_aantal { get; set; }
}