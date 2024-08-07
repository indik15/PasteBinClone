using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Domain.Models
{
    public class UserPasteInfo
    {
        public Guid Id { get; set; }
        public int TotalPastes { get; set; }
        public int TotalPublicPastes { get; set; }
        public int TotalPrivatePastes { get; set; }
        public int TotalActivePastes { get; set; }
        public string UserId { get; set; }
    }
}
