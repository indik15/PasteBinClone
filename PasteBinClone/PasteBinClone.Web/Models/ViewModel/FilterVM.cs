namespace PasteBinClone.Web.Models.ViewModel
{
    public class FilterVM
    {
        public IEnumerable<CategoryVM> Categories { get; set; }
        public IEnumerable<ContentTypeVM> ContentTypes { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
    }
}
