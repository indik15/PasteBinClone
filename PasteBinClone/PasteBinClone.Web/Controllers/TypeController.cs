﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;
using System.Collections.Generic;

namespace PasteBinClone.Web.Controllers
{
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
            var response = await _baseService.GetAll<ResponseAPI>(RouteConst.TypeRoute);

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContentTypeVM typeVM)
        {
            var response = await _baseService.Post<ResponseAPI>(typeVM,RouteConst.TypeRoute);

            if(response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _baseService.GetById<ResponseAPI>(id, RouteConst.TypeRoute);

            if(response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a list of ContentTypes
                ContentTypeVM contentType = JsonConvert.DeserializeObject<ContentTypeVM>(response.Data.ToString());

                return View(contentType);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var response = await _baseService.Delete<ResponseAPI>(id, RouteConst.TypeRoute);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _baseService.GetById<ResponseAPI>(id, RouteConst.TypeRoute);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a list of ContentTypes
                ContentTypeVM contentType = JsonConvert.DeserializeObject<ContentTypeVM>(response.Data.ToString());

                return View(contentType);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContentTypeVM typeVM)
        {
            var response = await _baseService.Put<ResponseAPI>(typeVM, RouteConst.TypeRoute);

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
