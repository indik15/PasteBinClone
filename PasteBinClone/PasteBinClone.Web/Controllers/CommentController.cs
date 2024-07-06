using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Controllers
{
    public class CommentController(IBaseService baseService, IUserInfo userInfo) : Controller
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IUserInfo _userInfo = userInfo;

        [HttpPost]
        public async Task<IActionResult> Create(CommentVM comment)
        {
            string returnUrl = Request.Headers["Referer"].ToString();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            comment.UserId = _userInfo.GetUserId(accessToken);

            await _baseService.Post(comment, RouteConst.CommentRoute, accessToken);

            return Redirect(returnUrl);
        }
    }
}
