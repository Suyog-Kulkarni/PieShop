using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PieShop.Models;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCart _shoppingCart;
    private readonly IPieRepository _pieRepository;

    public ShoppingCartController(IShoppingCart shoppingCart, IPieRepository pieRepository)
    {
        _shoppingCart = shoppingCart;
        _pieRepository = pieRepository;
    }
    public ViewResult Index()
    {
        var list = _shoppingCart.GetShoppingCartItems().ToList();
        _shoppingCart.ShoppingCartItems = list;

       
        Console.WriteLine(_shoppingCart.ShoppingCartItems.Count);

        var ShoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart,_shoppingCart.GetShoppingCartTotal());


        return View(ShoppingCartViewModel);
    }

    public RedirectToActionResult AddtoCart(int id)
    {
        
        var selectedpie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == id );
       
        if (selectedpie is not null)
        {
            _shoppingCart.AddtoCart(selectedpie);
        }
        
        return RedirectToAction("Index","ShoppingCart");
    }
    public RedirectToActionResult RemoveFromCart(int Id) 
    {
        var selectedpie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId== Id);
        if(selectedpie is not null)
        {
            _shoppingCart.RemoveeFromCart(selectedpie);
        }
        return RedirectToAction("Index");
    }
}

