using System.ComponentModel.DataAnnotations;
namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Orkestgroep
{
    public int Orkest_id { get; set; }
    
    public int Blok_id { get; set; }
    
    public int divisie { get; set; }
    
    public string Orkestnaam { get; set; }
    
    public string Muziekstukken { get; set; }
    
    public int Cijfer { get; set; }
}