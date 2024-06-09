using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.ViewModels
{
    public class PasteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublic { get; set; }
        public DateTime LifeTime { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public int TypeId { get; set; }
        public string UserId { get; set; }
    }
}
