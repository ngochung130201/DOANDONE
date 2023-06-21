namespace Share.ViewModels;

public class OrderItemVM
{
 
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}