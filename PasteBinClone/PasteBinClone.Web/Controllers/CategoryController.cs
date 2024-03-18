using Microsoft.AspNetCore.Mvc;

namespace PasteBinClone.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
