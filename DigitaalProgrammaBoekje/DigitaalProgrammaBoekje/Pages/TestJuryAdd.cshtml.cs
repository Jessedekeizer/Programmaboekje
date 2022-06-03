using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;

namespace DigitaalProgrammaBoekje.Pages;

public class TestJuryAdd : PageModel
{
    private string Photo;
    private IWebHostEnvironment Environment;
    public IEnumerable<Jurylid> Jurylist { get; set; }

    [BindProperty] 
    public Jurylid Jurylid { get; set; }
    
    public TestJuryAdd(IWebHostEnvironment _environment)
    {
        Environment = _environment;
    }
    
    public void OnGet()
    {
        JurylidRepository jurylist = new JurylidRepository();
        Jurylist = jurylist.GetJury(1);
    }
    
    public IActionResult OnPostUpload(List<IFormFile> frontPosted)
    {
        string note = Request.Form["Text1"];
        
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


        JurylidRepository jury = new JurylidRepository();
        jury.AddJurylid(Jurylid.jury_naam, note, Photo);
        return RedirectToPage("/HomeScreenAdmin");
    }
}