using AutoMapper;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Services
{
    public class CategoryServices : ICategoryServices
    {

        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryServices(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateCategory(CategoryDto category)
        {
            Category category1 = _mapper.Map<Category>(category);

            return await _categoryRepository.Create(category1);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            return await _categoryRepository.Delete(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            IEnumerable<Category> categories = await _categoryRepository.Get();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByID(int id)
        {
            Category category = await _categoryRepository.GetById(id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);

            return await _categoryRepository.Update(category);
        }
    }
}
