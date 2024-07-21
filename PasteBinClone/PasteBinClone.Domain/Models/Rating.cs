using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Domain.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
        public string? UserId { get; set; }
        public ApiUser ApiUser { get; set; }
        public Guid PasteId { get; set; }
        public Paste Paste { get; set; }
    }
}
