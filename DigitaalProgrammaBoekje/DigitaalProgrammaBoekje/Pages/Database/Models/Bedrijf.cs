﻿namespace DigitaalProgrammaBoekje.Pages.Database.Models;
using System.ComponentModel.DataAnnotations;

public class Bedrijf
{
    [Required]
    public int bedrijf_id { get; set; }
    [Required]
    public string bedrijf_naam { get; set; }
    
    public string websitelink { get; set; }
    public string Foto_link { get; set; }
}