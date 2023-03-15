using PieShop.Models;

public class ShoppingCartViewModel
{
    public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal total)
    {
        ShoppingCart = shoppingCart;
        ShoopingCartTotal = total;
    }
    public IShoppingCart ShoppingCart { get; set; }
    public decimal ShoopingCartTotal { get; set; }

    
}

