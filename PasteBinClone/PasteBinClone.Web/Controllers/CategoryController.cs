using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;
using System.Text.Json.Serialization;

namespace PasteBinClone.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IBaseService _categoryService;

        public CategoryController(IBaseService baseService)
        {
            _categoryService = baseService;
        }

        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _categoryService.GetAll<ResponseAPI>(RouteConst.CategoryRoute,
                accessToken);

            if(response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a list of Categories
                List<CategoryVM> categories = JsonConvert.DeserializeObject<List<CategoryVM>>(response.Data.ToString());

                return View(categories);
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _categoryService.Post<ResponseAPI>(categoryVM, 
                RouteConst.CategoryRoute,accessToken);

            if(response != null && response.IsSuccess)
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _categoryService.GetById<ResponseAPI>(id, 
                RouteConst.CategoryRoute, accessToken);

            if(response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Category
                CategoryVM categories = JsonConvert.DeserializeObject<CategoryVM>(response.Data.ToString());

                return View(categories);
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _categoryService.Put<ResponseAPI>(categoryVM, 
                RouteConst.CategoryRoute, accessToken);

            if (response != null && response.IsSuccess)
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _categoryService.GetById<ResponseAPI>(id, 
                RouteConst.CategoryRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Category
                CategoryVM categories = JsonConvert.DeserializeObject<CategoryVM>(response.Data.ToString());

                return View(categories);
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
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _categoryService.Delete<ResponseAPI>(id, 
                RouteConst.CategoryRoute, accessToken);

            if (response != null && response.IsSuccess)
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
