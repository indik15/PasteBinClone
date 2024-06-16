using Microsoft.AspNetCore.Mvc.Rendering;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<PasteVM> PasteVMs { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> ContentTypes { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}
