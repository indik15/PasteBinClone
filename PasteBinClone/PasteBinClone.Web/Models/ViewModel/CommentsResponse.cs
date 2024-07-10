namespace PasteBinClone.Web.Models.ViewModel
{
    public class CommentsResponse
    {
        public IEnumerable<CommentVM> CommentVMs { get; set; }
        public int TotalPages { get; set; }
    }
}
