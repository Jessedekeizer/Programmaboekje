using System.ComponentModel.DataAnnotations;

namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Gebruiker
{
    [Required]
    public int gebruiker_id { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public string naam { get; set; }
    [Required]
    public string wachtwoord { get; set; }
    [Required]
    public  char functie { get; set; }
    [Required]
    public int telefoonnummer { get; set; }
    [Required]
    public string dirigent { get; set; }
}