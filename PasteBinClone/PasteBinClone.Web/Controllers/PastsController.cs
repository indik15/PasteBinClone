using Microsoft.AspNetCore.Mvc;

namespace PasteBinClone.Web.Controllers
{
    public class PastsController : Controller
    {
        public IActionResult CreatePasts()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
