using System.ComponentModel.DataAnnotations;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class LanguageVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The  Language field name is required.")]
        [MaxLength(50)]
        public string LanguageName { get; set; }
    }
}
