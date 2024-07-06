using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.Services;
using PasteBinClone.Application.ViewModels;
using Serilog;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
    [Route("api/filter/language")]
    [Authorize(Roles = "Admin")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        private readonly IValidator<LanguageDto> _validator;
        private ResponseAPI _response;

        public LanguageController(ILanguageService languageService,
            IMapper mapper,
            IValidator<LanguageDto> validator)
        {
            _languageService = languageService;
            _mapper = mapper;
            _validator = validator;
            _response = new ResponseAPI();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI>> Get()
        {
            Log.Information("Request to receive all objects");

            IEnumerable<LanguageDto> languages = await _languageService.GetAllLanguage();

            if(languages == null)
            {
                return NotFound();
            }
            else
            {
                var languageVm = _mapper.Map<IEnumerable<LanguageVM>>(languages);
                _response.Data = languageVm;

                return Ok(_response);
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(int id)
        {
            Log.Information("Request to receive object by id: {@id}", id);

            LanguageDto languageDto = await _languageService.GetLanguageByID(id);

            if(languageDto == null)
            {
                return NotFound();
            }
            else
            {
                var languageVM = _mapper.Map<LanguageVM>(languageDto);
                _response.Data = languageVM;

                return Ok(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI>> Post([FromBody] LanguageDto languageDto)
        {
            Log.Information("Request to create an object");

            var valid = _validator.Validate(languageDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _languageService.CreateLanguage(languageDto);

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
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] LanguageDto languageDto)
        {
            Log.Information("Request to edit an object with id: {@id}", languageDto.Id);

            var valid = _validator.Validate(languageDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _languageService.UpdateLanguage(languageDto);

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

            bool result = await _languageService.DeleteLanguage(id);

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
