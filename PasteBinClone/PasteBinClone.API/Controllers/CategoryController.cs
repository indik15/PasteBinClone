using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using Serilog;
using System.Reflection;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryDto> _validator;
        private ResponseAPI _response;

        public CategoryController(ICategoryService categoryService,
            IMapper mapper,
            IValidator<CategoryDto> validator)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _validator = validator;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI>> Get()
        {
            Log.Information("Request to receive all objects");

            IEnumerable<CategoryDto> categoryDtoList = await _categoryService.GetAllCategory();

            if (categoryDtoList == null)
            {
                return NotFound();
            }
            else
            {
                var categoryVM = _mapper.Map<IEnumerable<CategoryVM>>(categoryDtoList);
                _response.Data = categoryVM;

                return Ok(_response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(int id)
        {
            Log.Information("Request to receive object by id: {@id}", id);

            CategoryDto categoryDto = await _categoryService.GetCategoryByID(id);

            if (categoryDto == null)
            {
                return NotFound();
            }
            else
            {
                var categoryVM = _mapper.Map<CategoryVM>(categoryDto);
                _response.Data = categoryVM;

                return Ok(_response);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI>> Post([FromBody] CategoryDto categoryDto)
        {
            Log.Information("Request to create an object");

            var valid = _validator.Validate(categoryDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _categoryService.CreateCategory(categoryDto);

            if (result)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] CategoryDto categoryDto)
        {
            Log.Information("Request to edit an object with id: {@id}", categoryDto.Id);

            var valid = _validator.Validate(categoryDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _categoryService.UpdateCategory(categoryDto);

            if (result)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI>> Delete(int id)
        {
            Log.Information("Request to delete an object with id: {@id}", id);

            bool result = await _categoryService.DeleteCategory(id);

            if (result)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
