using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;
using System.Collections.Generic;

namespace PasteBinClone.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TypeController : Controller
    {
        private readonly IBaseService _baseService;

        public TypeController(IBaseService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.GetAll(RouteConst.TypeRoute, accessToken);

            if(response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a list of ContentTypes
                List<ContentTypeVM> contentTypes = JsonConvert.DeserializeObject<List<ContentTypeVM>>(response.Data.ToString());

                return View(contentTypes);
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
        public async Task<IActionResult> Create(ContentTypeVM typeVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Post(typeVM, 
                RouteConst.TypeRoute, accessToken);

            if(response != null && response.IsSuccess)
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
                RouteConst.TypeRoute, accessToken);

            if(response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a ContentType
                ContentTypeVM contentType = JsonConvert.DeserializeObject<ContentTypeVM>(response.Data.ToString());

                return View(contentType);
            }
            else
            {
                return NotFound();
            }
        }

        //Post - Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Delete(id, 
                RouteConst.TypeRoute, accessToken);

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
                RouteConst.TypeRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a ContentType
                ContentTypeVM contentType = JsonConvert.DeserializeObject<ContentTypeVM>(response.Data.ToString());

                return View(contentType);
            }
            else
            {
                return NotFound();
            }
        }

        //Post - Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ContentTypeVM typeVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Put(typeVM, 
                RouteConst.TypeRoute, accessToken);

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
