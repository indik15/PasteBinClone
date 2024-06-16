using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Controllers
{
    public class PasteController(IBaseService baseService) : Controller
    {
        private readonly IBaseService _baseService = baseService;

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
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

            var response = await _baseService.Post(pasteVM, RouteConst.PasteRoute, accessToken);

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
