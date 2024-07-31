﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;
using PasteBinClone.Web.Services;

namespace PasteBinClone.Web.Controllers
{
    public class ProfileController(IBaseService baseService,IUserInfo userInfo) : Controller
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IUserInfo _userInfo = userInfo;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserPastes(int pageNumber = 1)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            string userId = _userInfo.GetUserId(accessToken);

            var response = await _baseService.GetById(userId, RouteConst.ProfileRoute, accessToken, obj: pageNumber);

            if (response != null && response.IsSuccess)
            {
                ResponsePaste userPastes = JsonConvert.DeserializeObject<ResponsePaste>(response.Data.ToString());

                UserPastesVM pastesVM = new UserPastesVM
                {
                    PasteVMs = userPastes.Pastes,
                    PageNumber = pageNumber,
                    IsActiveRightArrow = userPastes.TotalPages > pageNumber ? true : false,
                    IsActiveLeftArrow = pageNumber > 1 ? true : false
                };

                return View(pastesVM);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
