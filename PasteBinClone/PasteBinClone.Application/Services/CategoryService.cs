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
    public class CategoryService : ICategoryService
    {

        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateCategory(CategoryDto category)
        {
            Category category1 = _mapper.Map<Category>(category);

            int? result = await _categoryRepository.Create(category1);

            if(result != 0 && result != null)
            {
                Log.Information("Object {@i} created successfully.", result);

                return true;
            }
            else
            {
                Log.Error("Error creating object.");

                return false;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            int? result = await _categoryRepository.Delete(id);

            if (result != 0 && result != null)
            {
                Log.Information("Object {@i} successfully deleted.", result);

                return true;
            }
            else
            {
                Log.Error("Object deletion error.");

                return false;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            IEnumerable<Category> categories = await _categoryRepository.Get();

            if (categories == null)
            {
                Log.Information("Object not found.");

                return null;
            }

            Log.Information("Received objects: {@Count}", categories.Count());

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByID(int id)
        {
            Category category = await _categoryRepository.GetById(id);

            if(category == null)
            {
                Log.Information("Object {@i} not found.", id);

                return null;
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);

            int? result = await _categoryRepository.Update(category);

            if (result != 0 && result != null)
            {
                Log.Information("Object {@i} updated.", result);

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
