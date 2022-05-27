using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;
using DigitaalProgrammaBoekje.Pages.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages.Database.Categories;

public class Create : PageModel
{
    [BindProperty]
    public Category Category { get; set; }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var createdCategory = new CategoryRepository().Add(Category);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return Redirect(nameof(Index));
    }
}