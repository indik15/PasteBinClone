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

            //if the creation was successful, the method will return true
            bool result = await _categoryRepository.Create(category1);

            if (result)
            {
                Log.Information("Object {@i} created successfully.", category.CategoryName);
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
            //if the deletion was successful, the method will return true
            bool result = await _categoryRepository.Delete(id);

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

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            IEnumerable<Category> categories = await _categoryRepository.Get();

            if (categories == null)
            {
                Log.Information("Object not found.");

                return Enumerable.Empty<CategoryDto>();
            }

            Log.Information("Received objects: {@Count}", categories.Count());

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByID(int id)
        {
            Category category = await _categoryRepository.GetById(id);

            if(category == null)
            {
                throw new KeyNotFoundException();
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);

            //if the update was successful, the method will return true
            bool result = await _categoryRepository.Update(category);

            if (result)
            {
                Log.Information("Object {@i} updated.", categoryDto.Id);
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
