using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;
using System.Diagnostics;

namespace PieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPieRepository _ieRepository;

        public HomeController(ILogger<HomeController> logger, IPieRepository ieRepository)
        {
            _logger = logger;
            _ieRepository = ieRepository;
        }

        public IActionResult Index()
        {
            var piesoftheweek = _ieRepository.PiesOfTheWeek;

            var homeViewModel = new HomeViewModel(piesoftheweek);// can directly pass ienum but use of viewmodel is reccommended

            return View(homeViewModel);
        }

       /* public IActionResult Privacy()
        {
            return View();
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}