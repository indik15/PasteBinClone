using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Models
{
    public class ResponsePaste
    {
        public IEnumerable<HomePasteVM> Pastes { get; set; }
        public int TotalPages { get; set; }
    }
}
