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

        //Get-Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            await _categoryService.Post<CategoryVM>(categoryVM);

            return RedirectToAction(nameof(Index));
        }

        //Get-Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryService.GetById<CategoryVM>(id);

            if(result != null)
            {
                return View(result);
            }
            else
            {
                return NotFound();
            }

        }

        //Post-Edit
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVM categoryVM)
        {
            await _categoryService.Put<CategoryVM>(categoryVM);

            return RedirectToAction(nameof(Index));
        }

        //Get-Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.GetById<CategoryVM>(id);

            if (result != null)
            {
                return View(result);
            }
            else
            {
                return NotFound();
            }
        }

        //Post-Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            _categoryService.Delete<CategoryVM>(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
