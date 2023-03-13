namespace PieShop.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemsId { get; set; }
        public Pie Pie { get; set; } = default!;
        public int Amount { get;set; }
        public string? ShoppingCartId { get; set; }

    }
}
