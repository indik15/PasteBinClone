namespace PasteBinClone.Web.Models.ViewModel
{
    public class CommentVM
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string UserName { get; set; } = "";
        public Guid PasteId { get; set; }
    }
}
