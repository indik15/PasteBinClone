using PasteBinClone.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface ILanguageService
    {
        Task<LanguageDto> GetLanguageByID(int id);
        Task<IEnumerable<LanguageDto>> GetAllLanguage();
        Task<bool> CreateLanguage(LanguageDto language);
        Task<bool> DeleteLanguage(int id);
        Task<bool> UpdateLanguage(LanguageDto language);
    }
}
