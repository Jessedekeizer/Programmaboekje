using DigitaalProgrammaBoekje.Helpers;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Renci.SshNet;

namespace DigitaalProgrammaBoekje.Pages;

public class HomeScreenAdmin : PageModel
{
    
        private string Photo;
        private int User;
        public int Year { get; set; }
        
        private IWebHostEnvironment Environment;
        public IEnumerable<Festival> Festivallist { get; set; }
        public IEnumerable<Festival> FestivalYears { get; set; }  

        [BindProperty] 
        public Festival Festival { get; set; }
        
        [ViewData]
        public string Logged_In { get; set; }

        public HomeScreenAdmin(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult OnGet([FromQuery] string Jaartal)
        {
            Logged_In = Convert.ToString(new Rolechecker(HttpContext.Session).Loged_in());
            Rolechecker rolechecker = new Rolechecker(HttpContext.Session);
            if (!rolechecker.Loged_in())
            {
                return RedirectToPage("/login");
            }
            
            if (rolechecker.checkUser())
            {
                return RedirectToPage("/HomeScreen");
            }
            
            FestivalRepository festivallist = new FestivalRepository();
            if (!rolechecker.checkAdmin())
            {
                if (Jaartal != null)
                {
                    Festivallist =
                        festivallist.GetFestivalUser(
                            Convert.ToInt32(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)),
                            DateTime.ParseExact(Jaartal, "yyyy", null));
                }
                else
                {
                    Festivallist = festivallist.GetFestivalUser(Convert.ToInt32(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID)), DateTime.Now);
                }
            }
            else
            {
                if (Jaartal != null)
                {
                    Festivallist =
                        festivallist.Getfestival(DateTime.ParseExact(Jaartal, "yyyy", null));
                }
                else
                {
                    Festivallist = festivallist.Getfestival(DateTime.Now);
                }   
            }
            Year = Convert.ToInt16(Jaartal);
            FestivalYears = festivallist.FestivalsGetYears();
            
            return Page();
        }

        public IActionResult OnPostUpdate(int festival_id)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            };
            Response.Cookies.Append("Festival_id", Convert.ToString(festival_id), cookieOptions);
            return RedirectToPage("/EditScreen");
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

            User = Convert.ToInt32(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID));
            FestivalRepository festival = new FestivalRepository();
            festival.AddFestival(Festival.Festival_naam, Festival.Festival_locatie, Festival.Festival_datum, Photo, User);
            return RedirectToPage("/HomeScreenAdmin");
        }
    }
