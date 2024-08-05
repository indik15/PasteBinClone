using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBinClone.Application.Dto;
using PasteBinClone.Domain.Models;

namespace PasteBinClone.Application.Interfaces
{
    public interface IPasteRepository
    {
        Task<bool> Create(Paste paste);
        Task<Paste> GetById(Guid id);
        Task<(IEnumerable<Paste> pastes, int totalPaste)> Get(HomePasteRequestDto pasteRequestDto);
        Task<bool> Update(Paste paste);
        Task<bool> Delete(Guid id);
        Task<bool> DeleteRange(IEnumerable<Paste> pastes);
        Task<IEnumerable<Paste>> GetTopRatedPastes();
        Task<(IEnumerable<Paste> pastes, int totalPaste)> GetAllUserPastes(string userId, int pageNumber);
        Task<IEnumerable<Paste>> GetFiveUserPastes(string userId);


    }
}
