using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages.Database.Products;

public class Update : PageModel
{
    public Product Product { get; set; }
    
    public void OnGet([FromQuery]int productId)
    {
        Product = new ProductRepository().Get(productId);
    }

    public IActionResult OnPost(Product product)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updatedCategory = new ProductRepository().Update(product);

        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}