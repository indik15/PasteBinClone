namespace PasteBinClone.Web.Models.ViewModel
{
    public class GetCommentsVM
    {
        public IEnumerable<CommentVM> Comments { get; set; }
        public string ReturnUrl { get; set; }
        public int PageNumber { get; set; }
        public string PasteId { get; set; }
        public bool IsActiveRightArrow { get; set; }
        public bool IsActiveLeftArrow { get; set; }

    }
}
