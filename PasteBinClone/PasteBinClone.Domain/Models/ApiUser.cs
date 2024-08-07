using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Domain.Models
{
    public class ApiUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public UserPasteInfo? UserPasteInfo { get; set; }
        public ICollection<Paste>? Pastes { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
    }
}
