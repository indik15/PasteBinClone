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
            var categoryTask = await _categoryRepository.Get();
            var languageTask = await _languageRepository.Get();
            var contentTypeTask = await _typeRepository.Get();


            IEnumerable<CategoryVM> categoriesVM = _mapper.Map<IEnumerable<CategoryVM>>(categoryTask);
            IEnumerable<LanguageVM> languagesVM = _mapper.Map<IEnumerable<LanguageVM>>(languageTask);
            IEnumerable<ContentTypeVM> contentTypesVM = _mapper.Map<IEnumerable<ContentTypeVM>>(contentTypeTask);

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
