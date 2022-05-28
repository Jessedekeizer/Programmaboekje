using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages.Databasestuff.Products;

public class Delete : PageModel
{
    public Product Product { get; set; }
    
    public void OnGet([FromRoute] int productId)
    {
        Product = new ProductRepository().Get(productId);
    }

    public IActionResult OnPostDelete([FromRoute]int productId)
    {
        bool success = new ProductRepository().Delete(productId);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}