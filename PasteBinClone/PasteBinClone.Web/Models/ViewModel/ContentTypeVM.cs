using System.ComponentModel.DataAnnotations;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class ContentTypeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Type field name is required.")]
        [MaxLength(50)]
        public string TypeName { get; set; }
    }
}
