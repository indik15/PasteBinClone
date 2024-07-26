﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Controllers
{
    public class RatingController(IBaseService baseService, IUserInfo userInfo) : Controller
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IUserInfo _userInfo = userInfo;

        [HttpPost]
        public async Task<IActionResult> Create(RatingVM rating)
        {               
            string returnUrl = Request.Headers["Referer"].ToString();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            rating.UserId = _userInfo.GetUserId(accessToken);

            var response = await _baseService.Put(rating, RouteConst.RatingRoute, accessToken);

            if(response != null && response.IsSuccess)
            {
                return Redirect(returnUrl);
            }

            return NotFound();
        }
    }
}
