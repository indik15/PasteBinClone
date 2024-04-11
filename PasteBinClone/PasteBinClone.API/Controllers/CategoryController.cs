using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(int id)
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

        [HttpPost]
        public async Task<ActionResult<ResponseAPI>> Post([FromBody] CategoryDto categoryDto)
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

        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] CategoryDto categoryDto)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI>> Delete(int id)
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

    }
}
