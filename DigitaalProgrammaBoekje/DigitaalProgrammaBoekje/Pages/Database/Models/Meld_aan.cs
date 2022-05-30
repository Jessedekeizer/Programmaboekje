using System.ComponentModel.DataAnnotations;
namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Meld_aan
{
    [Required]
    public int Gebruiker_id { get; set; }
    [Required]
    public int Festival_id { get; set; }
}