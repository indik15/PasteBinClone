using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Dto
{
    public class HomePasteDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
        public int TypeId { get; set; }
        public ContentType? Type { get; set; }
        public string UserId { get; set; }
    }
}
