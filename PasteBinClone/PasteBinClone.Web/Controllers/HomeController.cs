using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;
using System.Diagnostics;

namespace PasteBinClone.Web.Controllers
{
    public class HomeController(IBaseService baseService) : Controller
    {
        private readonly IBaseService _baseService = baseService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _baseService.GetAll(RouteConst.PasteRoute);

            if (response != null && response.IsSuccess)
            {
                ResponseFilterAndPastes responseObject = JsonConvert.DeserializeObject<ResponseFilterAndPastes>(response.Data.ToString());

                HomeVM homeVM = new()
                {
                    PasteVMs = responseObject.Pastes,
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
                    })
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
