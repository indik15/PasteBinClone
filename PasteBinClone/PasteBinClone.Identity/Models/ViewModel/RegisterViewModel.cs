using System.ComponentModel.DataAnnotations;

namespace PasteBinClone.Identity.Models.ViewModel
{
    public class RegisterViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public bool RememberMe { get; set; }
        public string? CaptchaKey { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
