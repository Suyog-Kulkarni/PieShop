﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PieShop.Areas.Identity.Data;
using System;
using System.Data;
using System.Data.SqlClient;


namespace PieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private SignInManager<PieShopApplicationUser> SignInManager;
        private UserManager<PieShopApplicationUser> UserManager;
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
        /*public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            BethanysPieShopDbContext context = services.GetService<BethanysPieShopDbContext>() ?? throw new Exception("Error initializing");
            *//*CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.SameSite=  SameSiteMode.Unspecified;
            cookieOptions.Secure = true;
            cookieOptions.HttpOnly = true;*//*
            string cartId = session?.GetString("CartID") ?? Guid.NewGuid().ToString();


            session?.SetString("CartId", cartId);


            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }*/
        /*public IEnumerable<ShoppingCartItem> TowriteWhere()
        {
            return _bethanysPieShopDbContext.ShoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCartId);
        }*/

        public void AddtoCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(p => p.Pie.PieId == pie.PieId /*&& p.ShoppingCartId == ShoppingCartId*/);
            
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
            var ShoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.SingleOrDefault(p => p.Pie.PieId == pie.PieId /*&& p.ShoppingCartId == ShoppingCartId*/);

            var localAmount = 0;

            if(ShoppingCartItem is not null)
            {
                if(ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount--;
                    localAmount = ShoppingCartItem.Amount;// check here
                }
                else
                {
                    _bethanysPieShopDbContext.ShoppingCartItems.Remove(ShoppingCartItem);
                }

            }
           
            _bethanysPieShopDbContext.SaveChanges();
            return localAmount;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            List<String> Email = new List<string>();
            ConfigurationManager manager = new ConfigurationManager();
            SqlConnection sqlConnection = new SqlConnection(manager.GetConnectionString("BethanysPieShopContextConnection"));
            sqlConnection.Open();
            string query = "SELECT Email FROM ShoppingCartItems";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Email.Add(reader.GetString("Email"));
                }
            }
            foreach(var item in Email)
            {
                if (item == UserManager.GetUserName((System.Security.Claims.ClaimsPrincipal)UserManager.Users)) 
                {
                    
                }
            }
            return ShoppingCartItems ??= _bethanysPieShopDbContext.ShoppingCartItems.Where(p => p.ShoppingCartId==ShoppingCartId).Include(p =>p.Pie).ToList();// to return shoppingcartitems list
            
        }

        public void ClearCart()
        {
            var cartItem = _bethanysPieShopDbContext.ShoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCartId);

            _bethanysPieShopDbContext.ShoppingCartItems.RemoveRange(cartItem);
            _bethanysPieShopDbContext.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
            var total = _bethanysPieShopDbContext.ShoppingCartItems.Where(p => p.ShoppingCartId==ShoppingCartId).Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
