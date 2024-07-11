using System.ComponentModel.DataAnnotations;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class CommentVM
    {
        public Guid Id { get; set; }
        [Required (ErrorMessage = "The field is required")]
        [MaxLength(1000)]
        public string Body { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string UserName { get; set; } = "";
        public Guid PasteId { get; set; }
    }
}
