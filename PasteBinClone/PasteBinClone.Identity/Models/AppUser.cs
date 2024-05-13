using Microsoft.AspNetCore.Identity;

namespace PasteBinClone.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
