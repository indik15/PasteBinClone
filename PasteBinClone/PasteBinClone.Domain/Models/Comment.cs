using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public ApiUser User { get; set; }
        public Guid PasteId { get; set; }
        public Paste Paste { get; set; }
    }
}
