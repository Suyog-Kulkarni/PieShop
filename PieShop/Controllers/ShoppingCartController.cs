using Microsoft.AspNetCore.Mvc;
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
        var list = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = list;

        var ShoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart,_shoppingCart.GetShoppingCartTotal());

        return View(ShoppingCartViewModel);
    }

    public RedirectToActionResult AddtoCart(int Id)
    {
        Console.WriteLine(Id);
        var selectedpie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId== Id);
        if(selectedpie is not null)
        {
            _shoppingCart.AddtoCart(selectedpie);
        }
        return RedirectToAction("Index");
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

