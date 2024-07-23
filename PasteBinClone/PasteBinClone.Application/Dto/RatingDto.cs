using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Dto
{
    public class RatingDto
    {
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
        public string? UserId { get; set; }
        public Guid PasteId { get; set; }
    }
}
