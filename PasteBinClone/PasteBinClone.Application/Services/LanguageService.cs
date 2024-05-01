using AutoMapper;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Language> _baseRepository;

        public LanguageService(IMapper mapper, IBaseRepository<Language> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }
        public async Task<bool> CreateLanguage(LanguageDto language)
        {
            Language language1 = _mapper.Map<Language>(language);

            //if the creation was successful, the method will return true
            bool result = await _baseRepository.Create(language1);

            if (result)
            {
                Log.Information("Object {@i} created successfully.", language.LanguageName);
                return true;
            }
            else
            {
                Log.Error("Error creating object.");
                return false;
            }
        }

        public async Task<bool> DeleteLanguage(int id)
        {
            //if the deletion was successful, the method will return true
            bool result = await _baseRepository.Delete(id);

            if (result)
            {
                Log.Information("Object {@i} successfully deleted.", id);
                return true;
            }
            else
            {
                Log.Error("Object deletion error.");
                return false;
            }
        }

        public async Task<IEnumerable<LanguageDto>> GetAllLanguage()
        {
            IEnumerable<Language> languages = await _baseRepository.Get();

            if(languages == null)
            {
                Log.Information("Object not found.");
                return null;
            }

            Log.Information("Received objects: {@Count}", languages.Count());
            return _mapper.Map<IEnumerable<LanguageDto>>(languages);
        }

        public async Task<LanguageDto> GetLanguageByID(int id)
        {
            Language language = await _baseRepository.GetById(id);

            if(language == null)
            {
                Log.Information("Object {@i} not found.", id);
                return null;
            }

            return _mapper.Map<LanguageDto>(language);
        }

        public async Task<bool> UpdateLanguage(LanguageDto language)
        {
            Language language1 = _mapper.Map<Language>(language);

            //if the update was successful, the method will return true
            bool result = await _baseRepository.Update(language1);

            if (result)
            {
                Log.Information("Object {@i} updated.", language.Id);
                return true;
            }
            else
            {
                Log.Error("Object update error.");
                return false;
            }
        }
    }
}
