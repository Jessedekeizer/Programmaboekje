using System.ComponentModel.DataAnnotations;
namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Blok
{
    public int Blok_id { get; set; }
    [Required]
    public int Festival_id { get; set; }
    [Required]
    public DateTime Begintijd { get; set; }
    [Required]
    public char Blok_type { get; set; }
    [Required]
    public int Bloknummer { get; set; }
    
    public string Tekstvak { get; set; }
}