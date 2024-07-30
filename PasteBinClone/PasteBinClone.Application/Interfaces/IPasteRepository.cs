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
        Task<(IEnumerable<Paste> pastes, int totalPaste)> Get(int pageNumber, int? typeFilter, int? categoryFilter, int? languageFilter, int? sortedByFilter);
        Task<bool> Update(Paste paste);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteRange(IEnumerable<Paste> pastes);
        Task<IEnumerable<Paste>> GetTopRatedPaste();
        Task<IEnumerable<Paste>> GetAllUserPaste(string userId);

    }
}
