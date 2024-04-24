using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using Serilog;
using FluentValidation;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
    [Route("api/filter/type")]
    public class ContentTypeController : ControllerBase
    {
        private readonly IContentTypeService _typeService;
        private readonly IMapper _mapper;
        private ResponseAPI _response;
        private readonly IValidator<ContentTypeDto> _validator;

        public ContentTypeController(IContentTypeService typeService,
            IMapper mapper,
            IValidator<ContentTypeDto> validator)
        {
            _typeService = typeService;
            _mapper = mapper;
            _response = new();
            _validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(int id)
        {
            Log.Information("Request to receive object by id: {@id}", id);

            ContentTypeDto result = await _typeService.GetContentTypeById(id);

            if(result == null)
            {
                return NotFound();
            }
            else
            {
                var contentTypeVm = _mapper.Map<ContentTypeVM>(result);
                _response.Data = contentTypeVm;

                return Ok(_response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI>> GetAll()
        {
            Log.Information("Request to receive all objects");

            IEnumerable<ContentTypeDto> contentTypeDtos = await _typeService.GetAllContentType();
            
            if(contentTypeDtos == null)
            {
                return NotFound();
            }
            else
            {
                var contentTypeVmList = _mapper.Map<IEnumerable<ContentTypeVM>>(contentTypeDtos);
                _response.Data = contentTypeVmList;

                return Ok(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI>> Post([FromBody] ContentTypeDto contentTypeDto)
        {
            Log.Information("Request to create an object");

            var validation = _validator.Validate(contentTypeDto);

            if (!validation.IsValid)
            {
                return ValidationProblem();
            }

            await _typeService.CreateContentType(contentTypeDto);

            return Ok(_response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] ContentTypeDto contentTypeDto)
        {
            Log.Information("Request to edit an object with id: {@id}", contentTypeDto.Id);

            var validation = _validator.Validate(contentTypeDto);

            if (!validation.IsValid)
            {
                return ValidationProblem();
            }

            bool result = await _typeService.UpdateContentType(contentTypeDto);

            if(!result)
            {
                return NotFound();
            }
            else
            {
                return Ok(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI>> Delete(int id)
        {
            Log.Information("Request to delete an object with id: {@id}", id);

            bool result = await _typeService.DeleteContentType(id);

            if (!result)
            {
                return NotFound();
            }
            else
            {
                return Ok(_response);
            }
        }
    }
}
