namespace PasteBinClone.Web.Models.ViewModel.Paste
{
    public class HomePasteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsPublic { get; set; }
        public ulong Likes { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public CategoryVM Category { get; set; }
        public ContentTypeVM ContentType { get; set; }
        public LanguageVM Language { get; set; }

        public TimeSpan Time => DateTime.Now - CreateAt;
    }
}
