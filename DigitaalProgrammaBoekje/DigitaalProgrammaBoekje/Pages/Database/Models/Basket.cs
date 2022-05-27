namespace DigitaalProgrammaBoekje.Pages.Database.Models;

public class Basket
{
    public int ProductId { get; set; }
    public int Amount { get; set; }

    public int ChangeAmount { get; set; }

    public string ProductName { get; set; }
    
    public decimal ProductPrice { get; set; }

}