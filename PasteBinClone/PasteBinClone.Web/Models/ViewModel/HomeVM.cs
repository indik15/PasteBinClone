using Microsoft.AspNetCore.Mvc.Rendering;
using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<HomePasteVM> PasteVMs { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> ContentTypes { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}
