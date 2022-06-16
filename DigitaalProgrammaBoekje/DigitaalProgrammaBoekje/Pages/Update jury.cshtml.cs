using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigitaalProgrammaBoekje.Helpers;

namespace DigitaalProgrammaBoekje.Pages;

public class Update_jury : PageModel
{

    private string Photo;
    private IWebHostEnvironment Environment;
    public IEnumerable<Jurylid> Jurylids { get; set; }

    public Update_jury(IWebHostEnvironment _environment)
    {
        Environment = _environment;
    }

    public IActionResult OnGet([FromQuery] int jury_id)
    {
        if (new Rolechecker(HttpContext.Session).Loged_in())
        {
            if (new Rolechecker(HttpContext.Session).checkUser())
            {
                RedirectToPage("/HomeScreen");
            }
        }
        else
        {
            RedirectToPage("/Login");
        }
        
        Jurylids = new JurylidRepository().Get1Jury(jury_id);
        return Page();
    }

    public IActionResult OnPostUpdateNaam([FromForm] string NaamUpd, [FromForm] int jury_id)
    {
        Jurylids = new JurylidRepository().UpdateNaamJury(jury_id, NaamUpd);

        return RedirectToPage(new {Jury_id = jury_id});
    }

    public IActionResult OnPostUpdateBio([FromForm] string BioUpd, [FromForm] int jury_id)
    {
        Jurylids = new JurylidRepository().UpdatebioJury(jury_id, BioUpd);

        return RedirectToPage(new {Jury_id = jury_id});
    }

    public IActionResult OnPostUpload(List<IFormFile> frontPosted, [FromForm] int jury_id)
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

        Jurylids = new JurylidRepository().UpdateFotoJury(jury_id, Photo);
        return RedirectToPage(new {Jury_id = jury_id});
    }

    public IActionResult OnPostTerug()
    {
        return RedirectToPage("/Bedrijven");
    }
}