using Microsoft.AspNetCore.Mvc;
using PasteBinClone.Web.Interfaces;

namespace PasteBinClone.Web.Controllers
{
    public class PastsController(IBaseService baseService) : Controller
    {
        private readonly IBaseService _baseService = baseService;

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            //var result = _baseService.GetById()
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
