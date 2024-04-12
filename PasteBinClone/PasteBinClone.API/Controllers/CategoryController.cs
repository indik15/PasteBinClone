using AutoMapper;
using FluentValidation;
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
    [Route("api/filter")]
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
        public async Task<ActionResult<ResponseAPI>> GetAll()
        {
            try
            {
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
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the request.");

                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(int id)
        {
            try
            {
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
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the request.");

                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI>> Post([FromBody] CategoryDto categoryDto)
        {
            try
            {
                var valid = _validator.Validate(categoryDto);

                if (!valid.IsValid)
                {
                    return ValidationProblem();
                }

                bool result = await _categoryService.CreateCategory(categoryDto);

                if (!result)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "An error occurred while processing the request.");

                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] CategoryDto categoryDto)
        {
            try
            {
                var valid = _validator.Validate(categoryDto);

                if (!valid.IsValid)
                {
                    return ValidationProblem();
                }

                bool result = await _categoryService.UpdateCategory(categoryDto);

                if (!result)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the request.");

                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI>> Delete(int id)
        {
            try
            {
                bool result = await _categoryService.DeleteCategory(id);

                if (!result)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the request.");

                return NotFound();
            }
        }

    }
}
