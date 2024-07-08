namespace PasteBinClone.Web.Models.ViewModel
{
    public class GetCommentsVM
    {
        public IEnumerable<CommentVM> Comments { get; set; }
        public string ReturnUrl { get; set; }
    }
}
