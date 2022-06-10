using System.Net.Mime;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class EditScreen : PageModel
{
    private string Photo;
    private IWebHostEnvironment Environment;
    public IEnumerable<Blok> Bloks { get; set; }
    public IEnumerable<Jurylid> Jurylist { get; set; }
    public IEnumerable<Jurylid> AllJurieslist { get; set; }
    public IEnumerable<Bedrijf> Bedrijflist { get; set; }
    public IEnumerable<Sponsort> Reclamelist { get; set; }
    


    [BindProperty] public Blok Blok { get; set; }

    [BindProperty] public Jurylid Jurylid { get; set; }

    public string Jurynaam { get; set; }
    public string Jurybio { get; set; }
    public string Juryfoto { get; set; }
    public string Text { get; set; }

    public string OrkestNaam { get; set; }

    public int Divisie { get; set; }

    public string Time { get; set; }

    public int Blok_id { get; set; } = 0;

    public int Orkest_id { get; set; } = 0;
    public int Jury_id { get; set; } = 0;
    public int Bedrijf_id { get; set; } = 0;

    public string active_pauze { get; set; }
    public string active_orkest { get; set; }
    public string active_tekstvak { get; set; }
    public string active_jury { get; set; }
    public string active_reclame { get; set; }
    
    public string bedrijf_naam { get; set; }


    public EditScreen(IWebHostEnvironment _environment)
    {
        Environment = _environment;
    }

    public IActionResult OnGet([FromQuery] int blok_id, [FromQuery] int jury_id, [FromQuery] int bedrijf_id)
    {
        Blok_id = blok_id;
        Jury_id = jury_id;
        Bedrijf_id = bedrijf_id;
        Bloks = new BlokRepository().GetAll();
        JurylidRepository jurylist = new JurylidRepository();
        Jurylist = jurylist.GetJury(1);
        AllJurieslist = jurylist.GetAllJury();
        SponsortRepository Sponsort = new SponsortRepository();
        Reclamelist = Sponsort.GetSponsors();
        BedrijfRepository bedrijf = new BedrijfRepository();
        Bedrijflist = bedrijf.GetAllbedrijf();
        if (Blok_id != null)
        {
            foreach (var blok in Bloks)
            {
                if (blok.Blok_id == Blok_id)
                {
                    switch (blok.Blok_type)
                    {
                        case 'p':
                            active_pauze = "show active";
                            Time = blok.Begintijd.ToString(@"hh\:mm");
                            break;
                        case 't':
                            active_tekstvak = "show active";
                            Text = blok.Tekstvak;
                            break;
                        case 'o':
                            Time = blok.Begintijd.ToString(@"hh\:mm");
                            OrkestNaam = blok.Orkestgroep.Orkestnaam;
                            Divisie = blok.Orkestgroep.divisie;
                            Orkest_id = blok.Orkestgroep.Orkest_id;
                            Text = blok.Orkestgroep.Muziekstukken;
                            active_orkest = "show active";
                            break;
                    }
                }
            }
        }

        if (Jury_id != null)
        {
            foreach (var jury in Jurylist)
            {
                if (jury.jury_id == Jury_id)
                {
                    active_jury = "show active";
                    Jurynaam = jury.jury_naam;
                    Jurybio = jury.jury_bio;
                    Juryfoto = jury.jury_foto;
                }
            }
        }
        if (Bedrijf_id != null)
        {
            foreach (var bedrijfs in Bedrijflist)
            {
                if (bedrijfs.bedrijf_id == Bedrijf_id)
                {
                    active_reclame = "show active";
                    bedrijf_naam = bedrijfs.bedrijf_naam;
                }
            }
        }

        return Page();
    }

    public IActionResult OnPostUp(int blok_id, int bloknummer, int festival_id)
    {
        BlokRepository BlokCommand = new BlokRepository();
        BlokCommand.ChangeRankingUp(bloknummer, blok_id, festival_id);
        return RedirectToPage();
    }

    public IActionResult OnPostDown(int blok_id, int bloknummer, int festival_id)
    {
        BlokRepository BlokCommand = new BlokRepository();
        BlokCommand.ChangeRankingDown(bloknummer, blok_id, festival_id);
        return RedirectToPage();
    }

    public IActionResult OnPostUpdate(int blok_id)
    {
        BlokRepository BlokCommand = new BlokRepository();
        return RedirectToPage(new {Blok_id = blok_id});
    }

    public IActionResult OnPostSave()
    {
        string note = Request.Form["Text1"];
        BlokRepository blokCommand = new BlokRepository();
        if (Blok.Blok_type != 'o')
        {
            blokCommand.AddBlok(Blok.Blok_id, 1, Blok.Begintijd, Blok.Blok_type, note);
        }
        else
        {
            note = Request.Form["Text2"];
            new OrkestgroepRepository().AddOrkestgroep(note, Blok.Orkestgroep.Orkestnaam,
                Blok.Orkestgroep.divisie, Blok.Orkestgroep.Cijfer, Blok.Orkestgroep.Orkest_id,
                Blok.Blok_id, 1, Blok.Begintijd, Blok.Blok_type);
        }

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int blok_id, int bloknummer)
    {
        OrkestgroepRepository OrkestCommand = new OrkestgroepRepository();
        int Orkest_id = OrkestCommand.CheckIfOrkestgroep(blok_id);
        if (Orkest_id == 0)
        {
            new BlokRepository().DeleteBlok(blok_id);
        }
        else
        {
            OrkestCommand.DeleteOrkestgroep(Orkest_id, blok_id, bloknummer);
        }

        return RedirectToPage();
    }

    public IActionResult OnPostUpdatejury(int jury_id)
    {
        JurylidRepository JuryCommand = new JurylidRepository();
        return RedirectToPage(new {Jury_id = jury_id});
    }

    public IActionResult OnPostUnassignjury(int jury_id)
    {
        JurylidRepository JuryCommand = new JurylidRepository();
        JuryCommand.UnassignJury(jury_id);
        return RedirectToPage();
    }

    public IActionResult OnPostAddjury(List<IFormFile> frontPosted)
    {
        string note = Request.Form["TextJury"];

        string path = Path.Combine(this.Environment.WebRootPath, "content");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        foreach (IFormFile postedFile in frontPosted)
        {
            string fileName = null;
            int i = 0;
            string extension = Path.GetExtension(postedFile.FileName);
            while (i == 0)
            {
                fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, extension);

                if (!System.IO.File.Exists(fileName))
                {
                    i++;
                    Photo = fileName;
                }
            }

            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }
        }

        if (Jurylid.jury_foto != null)
        {
            Photo = Jurylid.jury_foto;
        }

        JurylidRepository jury = new JurylidRepository();
        jury.AddJurylid(Jurylid.jury_id, Jurylid.jury_naam, note, Photo, 1);
        return RedirectToPage();
    }

    public IActionResult OnPostAddExistingjury()
    {
        Photo = Jurylid.jury_foto;

        JurylidRepository jury = new JurylidRepository();
        jury.AddExistingjury(Jurylid.jury_id, 1);
        return RedirectToPage();
    }

    public IActionResult OnPostAddReclame(List<IFormFile> frontPosted, [FromForm] int bedrijf_id,[FromForm] string bedrijf_naam)
    {
        string path = Path.Combine(this.Environment.WebRootPath, "content");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        foreach (IFormFile postedFile in frontPosted)
        {
            string fileName = null;
            int i = 0;
            string extension = Path.GetExtension(postedFile.FileName);
            while (i == 0)
            {
                fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, extension);

                if (!System.IO.File.Exists(fileName))
                {
                    i++;
                    Photo = fileName;
                }
            }

            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }
        }
        
        new SponsortRepository().AddSponsor(bedrijf_id, 1, Photo);
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateCijfer(int Orkest_id, int Number)
    {
        new OrkestgroepRepository().UpdateCijfer(Orkest_id, Number);
        
        return RedirectToPage();
    }
}