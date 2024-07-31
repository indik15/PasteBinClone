using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class UserPastesVM
    {
        public int PageNumber { get; set; }
        public bool IsActiveRightArrow { get; set; }
        public bool IsActiveLeftArrow { get; set; }
        public IEnumerable<HomePasteVM> PasteVMs { get; set; }
    }
}
