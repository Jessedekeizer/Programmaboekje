using System.ComponentModel.DataAnnotations;

namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Jurylid
{
    [Required]
    public int jury_id { get; set; }
    [Required]
    public string jury_naam { get; set; }
    
    public string jury_bio { get; set; }
}