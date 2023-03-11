using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieRepository _ieRepository;

        public PieController(ICategoryRepository categoryRepository, IPieRepository pieRepository) 
        // the instances will be injected as we have registered our services
        {
            _categoryRepository = categoryRepository;
            _ieRepository = pieRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ViewResult List()
        {
            /*ViewBag.CurrentCategory = "Cheese cake";
            return View(_ieRepository.AllPies);*/
            
            PieListViewModel pieListViewModel = new PieListViewModel(_ieRepository.AllPies,"Cheese cake");
            return View(pieListViewModel);
        }
    }
}
