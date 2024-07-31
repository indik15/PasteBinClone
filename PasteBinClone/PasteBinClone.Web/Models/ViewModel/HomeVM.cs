using Microsoft.AspNetCore.Mvc.Rendering;
using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class HomeVM
    {
        public int PageNumber { get; set; }
        public bool IsActiveRightArrow { get; set; }
        public bool IsActiveLeftArrow { get; set; }
        public IEnumerable<HomePasteVM> PasteVMs { get; set; }
        public IEnumerable<HomePasteVM> UserPasteVMs { get; set; }
        public IEnumerable<HomePasteVM> TopRatedPasteVMs { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> ContentTypes { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}
