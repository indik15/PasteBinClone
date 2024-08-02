using System.ComponentModel.DataAnnotations;

namespace PasteBinClone.Identity.Models.ViewModel
{
    public class LoginViewModel
    {     
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        public string? CaptchaKey { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
