using System.ComponentModel.DataAnnotations;
namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Orkestgroep
{
    public int Orkest_id { get; set; }
    [Required]
    public int Blok_id { get; set; }
    [Required]
    public string Orkestnaam { get; set; }
    
    public int Cijfer { get; set; }
}