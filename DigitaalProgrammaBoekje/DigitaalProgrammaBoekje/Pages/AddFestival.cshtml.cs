using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class Add_Festival : PageModel
{
    private string Photo;
    private IWebHostEnvironment Environment;
    
    [BindProperty] 
    public Festival Festival { get; set; }

    public Add_Festival(IWebHostEnvironment _environment)
    {
        Environment = _environment;
    }
    public void OnGet()
    {
        
    }

    public IActionResult OnPostUpload(List<IFormFile> frontPosted)
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


        FestivalRepository festival = new FestivalRepository();
        festival.AddFestival(Festival.Festival_naam, Festival.Festival_locatie, Festival.Festival_datum, Photo, 1);
        return RedirectToPage("/HomeScreenAdmin");
    }
}