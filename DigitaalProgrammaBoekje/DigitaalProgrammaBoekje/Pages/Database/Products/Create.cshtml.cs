using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages.Databasestuff.Products;

public class Create : PageModel
{
    [BindProperty]
    public Product Product { get; set; }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var createdProduct = new ProductRepository().Add(Product);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return Redirect(nameof(Index));
    }
}