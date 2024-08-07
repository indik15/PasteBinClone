using Microsoft.AspNetCore.Identity;

namespace PasteBinClone.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }

    }
}
