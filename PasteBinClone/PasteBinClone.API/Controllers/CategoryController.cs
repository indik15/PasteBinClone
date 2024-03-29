using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public CategoryController(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryVM>>> GetAll()
        {
           IEnumerable<CategoryDto> categoryDtoList = await _categoryServices.GetAllCategory();
           
           return Ok(_mapper.Map<IEnumerable<CategoryVM>>(categoryDtoList));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVM>> Get(int id)
        {
            CategoryDto categoryDto = await _categoryServices.GetCategoryByID(id);

            return Ok(_mapper.Map<CategoryVM>(categoryDto));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            await _categoryServices.CreateCategory(categoryDto);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CategoryDto categoryDto)
        {
            await _categoryServices.UpdateCategory(categoryDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryServices.DeleteCategory(id);

            return Ok();
        }

    }
}
