using System.ComponentModel.DataAnnotations;

namespace PasteBinClone.Web.Models.ViewModel.Paste
{
    public class PasswordVM
    {
        public Guid PasteId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
