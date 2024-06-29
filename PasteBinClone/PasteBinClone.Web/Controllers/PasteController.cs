using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Models.ViewModel.Paste;
using PasteBinClone.Web.Request;
using System.IdentityModel.Tokens.Jwt;

namespace PasteBinClone.Web.Controllers
{
    public class PasteController(IBaseService baseService) : Controller
    {
        private readonly IBaseService _baseService = baseService;

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, string password = null)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.GetById(id, RouteConst.PasteRoute, accessToken, password);

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
                return NotFound();
            }
        }

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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = await _baseService.GetAll(RouteConst.FilterRoute);

            if(response != null && response.IsSuccess)
            {
                FilterVM filterVM = JsonConvert.DeserializeObject<FilterVM>(response.Data.ToString());

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
        [HttpPost]
        public async Task<IActionResult> Create(PasteVM pasteVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            if (!string.IsNullOrEmpty(accessToken))
            {
                var handler = new JwtSecurityTokenHandler();

                var jwtToken = handler.ReadToken(accessToken) as JwtSecurityToken;

                var userId = jwtToken.Claims.FirstOrDefault(u => u.Type == "sub").Value;

                pasteVM.UserId = userId;
            }

            switch (pasteVM.ExpireType)
            {
                case "1":
                    pasteVM.ExpireAt = pasteVM.ExpireAt.AddMinutes(1);
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
                //var getCreatedPaste = await _baseService.GetById();
                //return View();

                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                return NotFound();
            }
        }

        //Get-Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.GetById(id, RouteConst.PasteRoute, accessToken);

            if(response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Paste
                PasteVM paste = JsonConvert.DeserializeObject<PasteVM>(response.Data.ToString());

                return View(paste);
            }
            else
            {
                return NotFound();
            }
        }

        //Post-Edit
        [HttpPost]
        public async Task<IActionResult> Edit(PasteVM pasteVM)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Put(pasteVM, RouteConst.PasteRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                //var getCreatedPaste = await _baseService.GetById();
                //return View();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        //Get-Delete
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.GetById(id, RouteConst.PasteRoute, accessToken);

            if (response != null && response.IsSuccess)
            {
                //Deserialization of the received object into a Paste
                PasteVM paste = JsonConvert.DeserializeObject<PasteVM>(response.Data.ToString());

                return View(paste);
            }
            else
            {
                return NotFound();
            }
        }

        //Post-Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePaste(Guid id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _baseService.Delete(id, RouteConst.PasteRoute, accessToken);

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
