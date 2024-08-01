using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;
using PasteBinClone.Web.Services;

namespace PasteBinClone.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguageController : Controller
    {
        private readonly IBaseService _baseService;
        private readonly IDistributedCache _cache;
        public LanguageController(IBaseService baseService, IDistributedCache cache)
        {
            _baseService = baseService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.GetAll(RouteConst.LanguageRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a list of Languages
                List<LanguageVM> languages = JsonConvert.DeserializeObject<List<LanguageVM>>(response.Data.ToString());

                return View(languages);
            }
            else
            {
                return NotFound();
            }
        }

        //Get - Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post - Create
        [HttpPost]
        public async Task<IActionResult> Create(LanguageVM languageVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Post(languageVM, 
                RouteConst.LanguageRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
        //Get - Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.GetById(id, 
                RouteConst.LanguageRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Language
                LanguageVM language = JsonConvert.DeserializeObject<LanguageVM>(response.Data.ToString());

                return View(language);
            }
            else
            {
                return NotFound();
            }
        }

        //Post - Edit
        [HttpPost]
        public async Task<IActionResult> Edit(LanguageVM languageVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Put(languageVM, 
                RouteConst.LanguageRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        //Get - Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.GetById(id, 
                RouteConst.LanguageRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Language
                LanguageVM language = JsonConvert.DeserializeObject<LanguageVM>(response.Data.ToString());

                return View(language);
            }
            else
            {
                return NotFound();
            }
        }

        //Post - Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Delete(id, 
                RouteConst.LanguageRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                _cache.Remove("filters");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
