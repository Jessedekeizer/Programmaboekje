using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;

public class HomeScreenAdmin : PageModel
{
    
        private string Photo;
        private IWebHostEnvironment Environment;
        public IEnumerable<Festival> Festivallist { get; set; }

        [BindProperty] 
        public Festival Festival { get; set; }

        public HomeScreenAdmin(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public void OnGet()
        {
            FestivalRepository festivallist = new FestivalRepository();
            
        }

        public IActionResult OnPostAddFestival()
        {
            return Page();
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
