using System.ComponentModel.DataAnnotations;
namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Festival
{
    public int Festival_id { get; set; }
    [Required, MinLength(2), MaxLength(128)]
    public string Festival_naam { get; set; }
    [Required, MinLength(2), MaxLength(128)]
    public string Festival_locatie { get; set; }
    [Required]
    public DateTime Festival_datum { get; set; }
    [Required]
    public string Festival_logo { get; set; }
    
    public int Gebruiker_id { get; set; }
    
    public ICollection<Blok> Bloks { get; set; }
}