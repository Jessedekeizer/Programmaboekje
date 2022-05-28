using System.Collections;
using System.Collections.Generic;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages.Database.Products;

public class Index : PageModel
{
    public IEnumerable<Product> Products { get; set; }

    public IActionResult OnGet()
    {
        string check = HttpContext.Session.GetString("id");
        if (check == null)
        {
            return RedirectToPage("/Accounts/Login");
        }

        Products = new ProductRepository().Get();
        return Page();
    }
}