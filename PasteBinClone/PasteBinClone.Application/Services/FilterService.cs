using AutoMapper;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Services
{
    public class FilterService(IBaseRepository<Category> categoryRepository, 
        IBaseRepository<Language> languageRepository, 
        IBaseRepository<ContentType> typeRepository,
        IMapper mapper) : IFilterService
    {
        private readonly IBaseRepository<Category> _categoryRepository = categoryRepository;
        private readonly IBaseRepository<Language> _languageRepository = languageRepository;
        private readonly IBaseRepository<ContentType> _typeRepository = typeRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<FilterVM> GetAllFilters()
        {
            var category = await _categoryRepository.Get();
            var language = await _languageRepository.Get();
            var contentType = await _typeRepository.Get();

            IEnumerable<CategoryVM> categoriesVM = _mapper.Map<IEnumerable<CategoryVM>>(category);
            IEnumerable<LanguageVM> languagesVM = _mapper.Map<IEnumerable<LanguageVM>>(language);
            IEnumerable<ContentTypeVM> contentTypesVM = _mapper.Map<IEnumerable<ContentTypeVM>>(contentType);

            FilterVM filterVM = new()
            {
                Categories = categoriesVM,
                Languages = languagesVM,
                ContentTypes = contentTypesVM,
            };

            return filterVM;
        }
    }
}
