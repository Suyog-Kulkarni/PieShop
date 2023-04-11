using Microsoft.AspNetCore.Mvc;
/*using Microsoft.Identity.Client;*/
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
        public IActionResult List()
        {
            /*ViewBag.CurrentCategory = "Cheese cake";
            return View(_ieRepository.AllPies);*/
            
            PieListViewModel pieListViewModel = new PieListViewModel(_ieRepository.AllPies,"All Pies");
            return View(pieListViewModel);

            
        }
        public IActionResult Details(int id)
        {
            var pie = _ieRepository.GetPieById(id);
            if (pie is null)
            {

                return NotFound();
            }

            return View(pie);// returning the pie object to the view 
            
        }
    }
}
