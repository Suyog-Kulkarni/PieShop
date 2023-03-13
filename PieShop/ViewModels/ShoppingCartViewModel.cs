using PieShop.Models;

public class ShoppingCartViewModel
{
    public IShoppingCart? ShoppingCart { get; set; }
    public decimal? ShoopingCartTotal { get; set; }

    public ShoppingCartViewModel(IShoppingCart? shoppingCart, decimal? total)
    {
        ShoppingCart = shoppingCart;
        ShoopingCartTotal = total;
    }
}

