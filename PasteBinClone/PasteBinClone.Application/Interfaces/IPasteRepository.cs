using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBinClone.Domain.Models;

namespace PasteBinClone.Application.Interfaces
{
    public interface IPasteRepository
    {
        Task<bool> Create(Paste paste);
        Task<Paste> GetById(Guid id);
        Task<IEnumerable<Paste>> Get();
        Task<bool> Update(Paste paste);
        Task<bool> Delete(Guid id);
    }
}
