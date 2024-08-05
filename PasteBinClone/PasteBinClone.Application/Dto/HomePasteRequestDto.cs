using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Dto
{
    public class HomePasteRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int? TypeFilter { get; set; }
        public int? CategoryFilter { get; set; }
        public int? LanguageFilter { get; set; }
        public int? SortedByFilter { get; set; }

    }
}
