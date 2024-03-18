using Microsoft.AspNetCore.Mvc;

namespace PasteBinClone.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
