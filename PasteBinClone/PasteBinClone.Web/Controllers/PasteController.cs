using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Models.ViewModel.Paste;
using PasteBinClone.Web.Request;
using System.IdentityModel.Tokens.Jwt;

namespace PasteBinClone.Web.Controllers
{
    public class PasteController(IBaseService baseService, 
        IUserInfo userInfo,
        IHomeService homeService,
        IDistributedCache cache) : Controller
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IUserInfo _userInfo = userInfo;
        private readonly IHomeService _homeService = homeService;
        private readonly IDistributedCache _cache = cache;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id, string password = null)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            string userId = _userInfo.GetUserId(accessToken);

            var response = await _baseService.GetById(id, RouteConst.PasteRoute, accessToken, userId:userId, password: password);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Paste

                GetPasteVM paste = JsonConvert.DeserializeObject<GetPasteVM>(response.Data.ToString());
                
                if(response.Errors != null && response.Errors.Count() > 0)
                {
                    return RedirectToAction("Password", new { id = id, validationError = response.Errors.FirstOrDefault() });
                }

                if (!paste.IsPublic && paste.Body == null)
                {
                    return RedirectToAction("Password", new { id = id });
                }

                return View(paste);
            }
            else
            {
                await _cache.RemoveAsync(userId);
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Password(Guid id, string validationError)
        {
            if (!string.IsNullOrEmpty(validationError))
            {
                ModelState.AddModelError("Password", validationError);
            }
            var model = new PasswordVM { PasteId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Password(PasswordVM model)
        {
            return RedirectToAction("Details", new { id = model.PasteId, password = model.Password });
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var filterVM = await _homeService.GetAllFilters();

            if(filterVM != null)
            {
                CreatePasteVM createPasteVM = new()
                {
                    Categories = filterVM.Categories.Select(u => new SelectListItem
                    {
                        Text = u.CategoryName,
                        Value = u.id.ToString()
                    }),
                    ContentTypes = filterVM.ContentTypes.Select(u => new SelectListItem
                    {
                        Text = u.TypeName,
                        Value = u.Id.ToString()
                    }),
                    Languages = filterVM.Languages.Select(u => new SelectListItem
                    {
                        Text = u.LanguageName,
                        Value = u.Id.ToString()
                    })
                };

                return View(createPasteVM);
            }
            return NotFound();
        }

        //Post-Create
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PasteVM pasteVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            string userId = _userInfo.GetUserId(accessToken);
            pasteVM.UserId = userId;

            switch (pasteVM.ExpireType)
            {
                case "1":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddMinutes(5);
                    break;
                case "2":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddMinutes(10);
                    break;
                case "3":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddMinutes(30);
                    break;
                case "4":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddHours(1);
                    break;
                case "5":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddDays(1);
                    break;
                case "6":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddDays(3);
                    break;
                case "7":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddDays(30);
                    break;
                default: pasteVM.ExpireAt = pasteVM.ExpireAt.AddMinutes(10);
                    break;
            }


            var response = await _baseService.Post(pasteVM, RouteConst.PasteRoute, accessToken);

            if (response != null && response.IsSuccess)
            {

                await _cache.RemoveAsync(userId);
                string pasteId = response.Data.ToString();                

                return RedirectToAction(nameof(Details), "Paste", new { Id = pasteId });
            }
            else
            {
                return NotFound();
            }
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            string userId = _userInfo.GetUserId(accessToken);

            var responseFilter = await _homeService.GetAllFilters();
            var responsePaste = await _baseService.GetById(id, RouteConst.PasteRoute, accessToken, userId: userId);

            if (responseFilter != null && responsePaste != null && responsePaste.IsSuccess)
            {
                GetPasteVM paste = JsonConvert.DeserializeObject<GetPasteVM>(responsePaste.Data.ToString());

                CreatePasteVM createPasteVM = new()
                {
                    Categories = responseFilter.Categories.Select(u => new SelectListItem
                    {
                        Text = u.CategoryName,
                        Value = u.id.ToString()
                    }),
                    ContentTypes = responseFilter.ContentTypes.Select(u => new SelectListItem
                    {
                        Text = u.TypeName,
                        Value = u.Id.ToString()
                    }),
                    Languages = responseFilter.Languages.Select(u => new SelectListItem
                    {
                        Text = u.LanguageName,
                        Value = u.Id.ToString()
                    }),
                    PasteVM = new PasteVM
                    {
                        Id = paste.Id,
                        Title = paste.Title,
                        Body = paste.Body,
                        CategoryId = paste.Category?.id,
                        LanguageId = paste.Language?.Id,
                        TypeId = paste.Type?.Id,
                        IsPublic = paste.IsPublic,
                        ExpireAt = paste.ExpireAt,
                        UserId = paste.UserId.ToString(),
                        CreateAt = paste.CreateAt
                    }
                };

                return View(createPasteVM);
            }
            return NotFound();
        }

        //Post-Edit
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PasteVM pasteVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");           

            var response = await _baseService.Put(pasteVM, RouteConst.PasteRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                var userId = _userInfo.GetUserId(accessToken);
                await _cache.RemoveAsync(userId);
                //var getCreatedPaste = await _baseService.GetById();

                return RedirectToAction(nameof(Details), "Paste", new { Id = pasteVM.Id });
            }
            else
            {
                return NotFound();
            }
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            string userId = _userInfo.GetUserId(accessToken);

            var response = await _baseService.GetById(id, RouteConst.PasteRoute, accessToken, userId: userId);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Paste
                GetPasteVM paste = JsonConvert.DeserializeObject<GetPasteVM>(response.Data.ToString());

                return View(paste);
            }
            else
            {
                return NotFound();
            }
        }

        //Post-Delete
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePaste(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Delete(id, RouteConst.PasteRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                var userId = _userInfo.GetUserId(accessToken);
                await _cache.RemoveAsync(userId);

                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
