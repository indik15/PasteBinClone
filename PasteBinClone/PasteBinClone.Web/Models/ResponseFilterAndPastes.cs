using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Models
{
    public class ResponseFilterAndPastes
    {
        public IEnumerable<HomePasteVM> Pastes { get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }
        public IEnumerable<ContentTypeVM> ContentTypes { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
        public int TotalPages { get; set; }
    }
}
