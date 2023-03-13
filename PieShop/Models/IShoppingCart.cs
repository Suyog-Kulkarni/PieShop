namespace PieShop.Models
{
    public interface IShoppingCart
    {
        void AddtoCart(Pie pie);
        int RemoveeFromCart(Pie pie);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }

        /*static abstract void hello();*/ // can also be static but use abstract to not give default implementation
                                          // for default throw exception
    }
}
