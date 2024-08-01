using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Models.ViewModel.Paste;
using PasteBinClone.Web.Request;
using System.Diagnostics;

namespace PasteBinClone.Web.Controllers
{
    public class HomeController(IBaseService baseService, 
        IUserInfo userInfo,
        IHomeService homeService) : Controller
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IUserInfo _userInfo = userInfo;
        private readonly IHomeService _homeService = homeService;

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            string typeFilter = HttpContext.Request.Query["type"];
            string categoryFilter = HttpContext.Request.Query["category"];
            string languageFilter = HttpContext.Request.Query["language"];
            string sortedByFilter = HttpContext.Request.Query["sortedBy"];

            var accessToken = await HttpContext.GetTokenAsync("access_token");
        
            //Getting pastes
            var response = await _baseService.GetAll(RouteConst.PasteRoute, data: new
            {
                TypeFilter = typeFilter,
                CategoryFilter = categoryFilter,
                LanguageFilter = languageFilter,
                SortedByFilter = sortedByFilter ?? "1",
                PageNumber = pageNumber
            });
           
            if (response != null && response.IsSuccess)
            {
                ResponsePaste pastes = JsonConvert.DeserializeObject<ResponsePaste>(response.Data.ToString());


                //If user is authorised we can get his pastes
                IEnumerable<HomePasteVM> userPaste = await _homeService.GetAllUserPastes(accessToken);

                //Get top 5 popular pastes
                IEnumerable<HomePasteVM> topRatedPastes = await _homeService.GetTopRatedPastes();

                //Getting filters
                FilterVM filter = await _homeService.GetAllFilters();


                HomeVM homeVM = new()
                {
                    PasteVMs = pastes.Pastes,
                    UserPasteVMs = userPaste,
                    TopRatedPasteVMs = topRatedPastes,
                    
                    Categories = filter.Categories.Select(u => new SelectListItem
                    {
                        Text = u.CategoryName,
                        Value = u.id.ToString()
                    }),
                    ContentTypes = filter.ContentTypes.Select(u => new SelectListItem
                    {
                        Text = u.TypeName,
                        Value = u.Id.ToString()
                    }),
                    Languages = filter.Languages.Select(u => new SelectListItem
                    {
                        Text = u.LanguageName,
                        Value = u.Id.ToString()
                    }),
                    PageNumber = pageNumber,
                    IsActiveRightArrow = pastes.TotalPages > pageNumber ? true : false,
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
    }
}
