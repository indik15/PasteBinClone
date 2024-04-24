using Microsoft.AspNetCore.Mvc;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
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
            var response = await _categoryService.GetAll<ResponseAPI<IEnumerable<CategoryVM>>>();

            if(response != null && response.IsSuccess == true)
            {
                IEnumerable<CategoryVM> categoryVM = response.Data;

                return View(categoryVM);
            }
            else
            {
                return NotFound();
            }
        }

        //Get-Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            var response = await _categoryService.Post<ResponseAPI<CategoryVM>>(categoryVM);

            if(response.IsSuccess == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        //Get-Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _categoryService.GetById<ResponseAPI<CategoryVM>>(id);

            if(response != null && response.IsSuccess == true)
            {
                return View(response.Data);
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
            var response = await _categoryService.Put<ResponseAPI<CategoryVM>>(categoryVM);

            if (response.IsSuccess == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        //Get-Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.GetById<ResponseAPI<CategoryVM>>(id);

            if (response != null && response.IsSuccess == true)
            {
                return View(response.Data);
            }
            else
            {
                return NotFound();
            }
        }

        //Post-Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var response = await _categoryService.Delete<ResponseAPI<CategoryVM>>(id);

            if (response != null && response.IsSuccess == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
