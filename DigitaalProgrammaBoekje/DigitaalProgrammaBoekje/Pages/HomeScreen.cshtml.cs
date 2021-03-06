using System.Globalization;
using DigitaalProgrammaBoekje.Helpers;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Renci.SshNet;

namespace DigitaalProgrammaBoekje.Pages;

public class HomeScreen : PageModel
{
   private string Photo;
   
           private IWebHostEnvironment Environment;
           public IEnumerable<Festival> Festivallist { get; set; }
           
           public IEnumerable<Festival> FestivalYears { get; set; }  
           
           public IEnumerable<Meld_aan> MeldAans { get; set; }


           [BindProperty] 
           public Festival Festival { get; set; }
   
          [ViewData]
          public string Logged_In { get; set; }
          
          public int Year { get; set; }
          
          public int Gebruiker_id { get; set; }
   
           
           public IActionResult OnGet([FromQuery]string Jaartal)
           {
               MeldAans = new Meld_aanRepository().GetAll();
               
               Logged_In = Convert.ToString(new Rolechecker(HttpContext.Session).Loged_in());
               if (Convert.ToBoolean(Logged_In))
               {
                   if (!new Rolechecker(HttpContext.Session).checkUser())
                   {
                       
                       return RedirectToPage("/HomeScreenAdmin");
                   }
                   Gebruiker_id = Convert.ToInt16(HttpContext.Session.GetString(SessionConstant.Gebruiker_ID));
               }
               Year = Convert.ToInt16(Jaartal);
               
               FestivalRepository festivallist = new FestivalRepository();
              

               
               if (Jaartal != null)
               {
                   Festivallist = festivallist.Getfestival(DateTime.ParseExact(Jaartal, "yyyy", null));
               }
               else
               {
                   Festivallist = festivallist.Getfestival(DateTime.Now);
               }
               FestivalYears = festivallist.FestivalsGetYears();
               return Page();
           }
   
           public IActionResult OnPostSignup([FromForm]int festival_id, [FromForm] int gebruiker_id)
           {
               new Meld_aanRepository().AddToFestival(festival_id, gebruiker_id);
               return RedirectToPage();
           }
           
           public IActionResult OnPostSignout([FromForm]int festival_id, [FromForm] int gebruiker_id)
           {
               new Meld_aanRepository().RemoveFromFestival(festival_id, gebruiker_id);
               return RedirectToPage();
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