namespace PasteBinClone.Web.Models.ViewModel.Paste
{
    public class GetPasteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public CategoryVM Category { get; set; }
        public ContentTypeVM Type { get; set; }
        public LanguageVM Language { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public TimeSpan TimeRemaining => ExpireAt - DateTime.Now;
    }
}
