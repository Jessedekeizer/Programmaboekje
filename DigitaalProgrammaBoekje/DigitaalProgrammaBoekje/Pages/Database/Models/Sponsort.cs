using System.ComponentModel.DataAnnotations;
namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Sponsort
{
    [Required]
    public int Bedrijf_id { get; set; }
    [Required]
    public int Festival_id { get; set; }
    [Required]
    public string Foto_link { get; set; }
}