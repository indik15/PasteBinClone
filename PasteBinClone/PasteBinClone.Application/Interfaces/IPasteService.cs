using PasteBinClone.Application.Dto;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IPasteService
    {
        Task<Guid> CreatePaste(PasteDto pasteDto);
        Task<(GetPasteDto getPasteDto, string errorMessage)> GetPasteById(Guid id, string password = null, string userId = null);
        Task<(IEnumerable<HomePasteDto> pastes, int totalPages)> GetAllPaste(HomePasteRequestDto pasteRequestDto);
        Task<bool> DeletePaste(Guid id);
        Task<bool> UpdatePaste(PasteDto pasteDto);
        Task<IEnumerable<HomePasteDto>> GetTopRatedPastes();
        Task<IEnumerable<HomePasteDto>> GetFiveUserPastes(string userId);
        Task<(IEnumerable<HomePasteDto> pastes, int totalPages)> GetAllUserPastes(string userId, int totalPaste);
    }
}
