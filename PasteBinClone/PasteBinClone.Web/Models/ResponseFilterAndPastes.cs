using PasteBinClone.Web.Models.ViewModel;

namespace PasteBinClone.Web.Models
{
    public class ResponseFilterAndPastes
    {
        public IEnumerable<PasteVM> Pastes { get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }
        public IEnumerable<ContentTypeVM> ContentTypes { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
    }
}
