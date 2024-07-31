using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Models.ViewModel.Paste;
using PasteBinClone.Web.Request;
using System.Diagnostics;

namespace PasteBinClone.Web.Controllers
{
    public class HomeController(IBaseService baseService, IUserInfo userInfo) : Controller
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IUserInfo _userInfo = userInfo;

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            string typeFilter = HttpContext.Request.Query["type"];
            string categoryFilter = HttpContext.Request.Query["category"];
            string languageFilter = HttpContext.Request.Query["language"];
            string sortedByFilter = HttpContext.Request.Query["sortedBy"];

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            IEnumerable<HomePasteVM> userPaste = null;
            IEnumerable<HomePasteVM> topRatedPastes = null;


            if (accessToken != null)
            {
                string userId = _userInfo.GetUserId(accessToken);

                var userPasteResponse = await _baseService.GetById(userId, RouteConst.HomeRoute, accessToken);

                if (userPasteResponse != null && userPasteResponse.IsSuccess)
                {
                    userPaste = JsonConvert.DeserializeObject<IEnumerable<HomePasteVM>>(userPasteResponse.Data.ToString());
                }
            }

            var topRatedPastesResponse = await _baseService.GetAll(RouteConst.HomeRoute);

            if (topRatedPastesResponse != null && topRatedPastesResponse.IsSuccess)
            {
                topRatedPastes = JsonConvert.DeserializeObject<IEnumerable<HomePasteVM>>(topRatedPastesResponse.Data.ToString());
            }

            var response = await _baseService.GetAll(RouteConst.PasteRoute, data: new
            {
                TypeFilter = typeFilter,
                CategoryFilter = categoryFilter,
                LanguageFilter = languageFilter,
                SortedByFilter = sortedByFilter,
                PageNumber = pageNumber
            });

            if (response != null && response.IsSuccess)
            {
                ResponseFilterAndPastes responseObject = JsonConvert.DeserializeObject<ResponseFilterAndPastes>(response.Data.ToString());

                HomeVM homeVM = new()
                {
                    PasteVMs = responseObject.Pastes,
                    UserPasteVMs = userPaste,
                    TopRatedPasteVMs = topRatedPastes,
                    
                    Categories = responseObject.Categories.Select(u => new SelectListItem
                    {
                        Text = u.CategoryName,
                        Value = u.id.ToString()
                    }),
                    ContentTypes = responseObject.ContentTypes.Select(u => new SelectListItem
                    {
                        Text = u.TypeName,
                        Value = u.Id.ToString()
                    }),
                    Languages = responseObject.Languages.Select(u => new SelectListItem
                    {
                        Text = u.LanguageName,
                        Value = u.Id.ToString()
                    }),
                    PageNumber = pageNumber,
                    IsActiveRightArrow = responseObject.TotalPages > pageNumber ? true : false,
                    IsActiveLeftArrow = pageNumber > 1 ? true : false
                };

                return View(homeVM);
            }
            else
            {
                return NotFound();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public IActionResult Signup()
        {
            return Redirect("https://localhost:44364/Account/Register");
        }

        public async Task<IActionResult> HomeUsersPastes()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            string userId = _userInfo.GetUserId(accessToken);

            var response = await _baseService.GetById(userId, RouteConst.HomeRoute, accessToken);

            if(response != null && response.IsSuccess)
            {
                IEnumerable<HomePasteVM> homePastes = JsonConvert.DeserializeObject<IEnumerable<HomePasteVM>>(response.Data.ToString());

                return View(homePastes);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
