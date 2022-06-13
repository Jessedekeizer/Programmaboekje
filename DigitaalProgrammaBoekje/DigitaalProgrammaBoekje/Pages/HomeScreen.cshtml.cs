using System.Globalization;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DigitaalProgrammaBoekje.Pages;

public class HomeScreen : PageModel
{
   private string Photo;
           private IWebHostEnvironment Environment;
           public IEnumerable<Festival> Festivallist { get; set; }
           
           public IEnumerable<Festival> FestivalYears { get; set; }  
           

           [BindProperty] 
           public Festival Festival { get; set; }
   
          [ViewData]
          public string Logged_In { get; set; }
   
           
           public void OnGet([FromQuery]string Jaartal)
           {
               Logged_In = Convert.ToString(new Rolechecker(HttpContext.Session).Loged_in());
               
               //string Jaartal = Request.Cookies["jaar"];
               FestivalRepository festivallist = new FestivalRepository();

               //Als settingsStr == null, dat betekend dat er geen cookie bestaat, dus maakt hij een nieuwe cookie aan voor settings.
               if (Jaartal != null)
               {
                   Jaartal = Jaartal + "-05-08 14:40:52";
                   Festivallist = festivallist.Getfestival(DateTime.Parse(Jaartal));
               }
               FestivalYears = festivallist.FestivalsGetYears();
               
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

           public IActionResult OnPostJaartje([FromForm] string jaar)
           {
               
               //Als settingsStr == null, dat betekend dat er geen cookie bestaat, dus maakt hij een nieuwe cookie aan voor settings.
               
                   Response.Cookies.Append("jaar", jaar, new CookieOptions()
                   {
                       Expires = DateTimeOffset.Now.AddDays(30)
                   });
                   
                  
               
               return Page();
           }
}