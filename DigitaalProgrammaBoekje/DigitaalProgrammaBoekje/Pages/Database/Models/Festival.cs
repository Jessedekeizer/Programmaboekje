using System.ComponentModel.DataAnnotations;
namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Festival
{
    [Required, MinLength(2), MaxLength(128)]
    public string Festival_naam { get; set; }
    [Required]
    public DateOnly Festival_datum { get; set; }
    [Required]
    public string Festival_logo { get; set; }
    
    public int Gebruiker_id { get; set; }
}