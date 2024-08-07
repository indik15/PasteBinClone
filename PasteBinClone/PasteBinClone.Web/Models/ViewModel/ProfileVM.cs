using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class ProfileVM
    {
        public ApiUserVM ApiUser { get; set; }
        public IEnumerable<HomePasteVM> Pastes { get; set; }
    }
}
