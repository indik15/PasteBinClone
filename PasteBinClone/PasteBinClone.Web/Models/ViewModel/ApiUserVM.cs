using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Models.ViewModel
{
    public class ApiUserVM
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string Email { get; set; }
        public UserPasteInfoVM UserPasteInfo { get; set; }

    }
}
