using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.ViewModels
{
    public class FilterVM
    {
        public IEnumerable<CategoryVM> Categories { get; set; }
        public IEnumerable<ContentTypeVM> ContentTypes { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
    }
}
