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
            var categoryTask = _categoryRepository.Get();
            var languageTask = _languageRepository.Get();
            var contentTypeTask = _typeRepository.Get();

            await Task.WhenAll(categoryTask, languageTask, contentTypeTask);

            var categories = await categoryTask;
            var languages = await languageTask;
            var contentTypes = await contentTypeTask;


            IEnumerable<CategoryVM> categoriesVM = _mapper.Map<IEnumerable<CategoryVM>>(categories);
            IEnumerable<LanguageVM> languagesVM = _mapper.Map<IEnumerable<LanguageVM>>(languages);
            IEnumerable<ContentTypeVM> contentTypesVM = _mapper.Map<IEnumerable<ContentTypeVM>>(contentTypes);

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
