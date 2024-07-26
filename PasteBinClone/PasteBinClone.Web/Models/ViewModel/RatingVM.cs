namespace PasteBinClone.Web.Models.ViewModel
{
    public class RatingVM
    {
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
        public string? UserId { get; set; }
        public Guid PasteId { get; set; }

        public ulong Likes { get; set; }
        public ulong Dislikes { get; set; }

        public bool CurrentIsLiked { get; set; }
        public bool CurrentIsDisliked { get; set; }
    }
}
