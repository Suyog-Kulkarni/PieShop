using Microsoft.EntityFrameworkCore;

namespace PieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } =  default!;

        public ShoppingCart(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }
        /*IserviceProvider is an interface that defines a mechanism for retrieving services or dependencies.
         The IServiceProvider interface defines a single method called GetService,
        which takes a Type object representing the service or dependency to retrieve 
        and returns an object that implements that service or dependency.*/

        /*HttpContext is an object in ASP.NET that represents the current HTTP request and response. It contains information about the request
        such as the URL, headers, and query string parameters, as well as information about the response,*/

        /*IHttpContextAccessors is an interface in ASP.NET Core that provides access to the current HttpContext.
         It is used to access the HttpContext instance in code where it is not directly available
        such as in a class or service that is not part of the request processing pipeline.*/
        public static ShoppingCart GetCart(IServiceProvider serviceProvider)
        {
            ISession? session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            BethanysPieShopDbContext context = serviceProvider.GetService<BethanysPieShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartID") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }
        public IEnumerable<ShoppingCartItem> TowriteWhere()
        {
            return _bethanysPieShopDbContext.ShoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCartId);
        }

        public void AddtoCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(p => p.Pie.PieId == pie.PieId && p.ShoppingCartId == ShoppingCartId);
            
            if(shoppingCartItem is null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };
                _bethanysPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
                
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethanysPieShopDbContext.SaveChanges();
        }
        public int RemoveeFromCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(p => p.Pie.PieId == pie.PieId && p.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem is not null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;// check here
                }
                else
                {
                    _bethanysPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }

            }
           
            _bethanysPieShopDbContext.SaveChanges();
            return localAmount;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _bethanysPieShopDbContext.ShoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCartId).Include(s => s.Pie).ToList();        // to return shoppingcartitems list
        }

        public void ClearCart()
        {
            var cartItem = TowriteWhere();

            _bethanysPieShopDbContext.ShoppingCartItems.RemoveRange(cartItem);
            _bethanysPieShopDbContext.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
            var total = TowriteWhere().Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
