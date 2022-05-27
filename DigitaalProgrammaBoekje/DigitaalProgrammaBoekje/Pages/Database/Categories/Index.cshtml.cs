using System.Collections;
using System.Collections.Generic;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages.Databasestuff.Categories;

public class Index : PageModel
{
    public IEnumerable<Category> Categories { get; set; }
    
    public IActionResult OnGet()
    {
        string check = HttpContext.Session.GetString("id");
        if (check == null)
        {return RedirectToPage("/Accounts/Login");}
        
        Categories = new CategoryRepository().Get();
        return Page();
    }
}