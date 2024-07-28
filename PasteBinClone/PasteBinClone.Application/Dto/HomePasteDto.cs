using PasteBinClone.Application.ViewModels;
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
        public ulong Likes { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public CategoryVM Category { get; set; }
        public ContentTypeVM ContentType { get; set; }
        public LanguageVM Language { get; set; }
    }
}
