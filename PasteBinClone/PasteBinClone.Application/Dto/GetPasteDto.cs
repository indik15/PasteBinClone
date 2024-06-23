using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Dto
{
    public class GetPasteDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public CategoryVM Category { get; set; }
        public ContentTypeVM ContentType { get; set; }
        public LanguageVM Language { get; set; }
        public Guid UserId { get; set; }    
        public string UserName { get; set; }
    }
}
