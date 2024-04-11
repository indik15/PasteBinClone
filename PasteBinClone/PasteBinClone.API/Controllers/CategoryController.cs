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

        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryDto> _validator;
        private ResponseAPI _response;

        public CategoryController(ICategoryServices categoryServices,
            IMapper mapper,
            IValidator<CategoryDto> validator)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
            _validator = validator;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI>> GetAll()
        {
            try
            {
                IEnumerable<CategoryDto> categoryDtoList = await _categoryServices.GetAllCategory();

                if (categoryDtoList == null)
                {
                    _response.IsSuccess = false;

                    return NotFound(_response);
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
                Log.Error(ex, "An error occurred while processing GetAll() request.");
                _response.IsSuccess = false;

                return NotFound(_response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(int id)
        {
            try
            {
                CategoryDto categoryDto = await _categoryServices.GetCategoryByID(id);


                if (categoryDto == null)
                {
                    _response.IsSuccess = false;

                    return NotFound(_response);
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
                Log.Error(ex, "An error occurred while processing GetCategoryByID() request.");
                _response.IsSuccess = false;

                return NotFound(_response);
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
                    _response.IsSuccess = false;

                    return NotFound(_response);
                }

                bool result = await _categoryServices.CreateCategory(categoryDto);

                if (!result)
                {
                    _response.IsSuccess = false;

                    return NotFound(_response);
                }
                else
                {
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "An error occurred while processing Post() request.");
                _response.IsSuccess = false;

                return NotFound(_response);
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
                    _response.IsSuccess = false;

                    return NotFound(_response);
                }

                bool result = await _categoryServices.UpdateCategory(categoryDto);

                if (!result)
                {
                    _response.IsSuccess = false;

                    return NotFound(_response);
                }
                else
                {
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing Put() request.");
                _response.IsSuccess = false;

                return NotFound(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI>> Delete(int id)
        {
            try
            {
                bool result = await _categoryServices.DeleteCategory(id);

                if (!result)
                {
                    _response.IsSuccess = false;

                    return NotFound(_response);
                }
                else
                {
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing Delete() request.");
                _response.IsSuccess = false;

                return NotFound(_response);
            }
        }

    }
}
