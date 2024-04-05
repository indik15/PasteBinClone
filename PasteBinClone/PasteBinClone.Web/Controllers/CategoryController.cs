using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;

namespace PasteBinClone.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBaseService _categoryService;

        public CategoryController(IBaseService baseService)
        {
            _categoryService = baseService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryVM> categoryVM = await _categoryService.GetAll<CategoryVM>();

            return View(categoryVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
