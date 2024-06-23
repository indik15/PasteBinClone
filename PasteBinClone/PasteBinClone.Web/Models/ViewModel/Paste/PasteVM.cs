namespace PasteBinClone.Web.Models.ViewModel.Paste
{
    public class PasteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublic { get; set; }
        public string ExpireType { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime ExpireAt { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public int TypeId { get; set; }
        public string UserId { get; set; }
    }
}
