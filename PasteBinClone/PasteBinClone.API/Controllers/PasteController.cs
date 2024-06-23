using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using Serilog;

namespace PasteBinClone.API.Controllers
{
    [Route("api/pastes")]
    [ApiController]
    public class PasteController(IPasteService pasteService,
        IMapper mapper,
        IValidator<PasteDto> validator,
        IFilterService filterService) : ControllerBase
    {
        private readonly IPasteService _pasteService = pasteService;
        private readonly IFilterService _filterService = filterService;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<PasteDto> _validator = validator;
        private readonly ResponseAPI _responseAPI = new();

        [HttpPost]
        public async Task<ActionResult<ResponseAPI>> Post([FromBody] PasteDto pasteDto)
        {
            Log.Information("Request to create a Paste");

            var valid = _validator.Validate(pasteDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);
                return ValidationProblem();
            }

            bool result = await _pasteService.CreatePaste(pasteDto);

            if (result)
            {
                return Ok(_responseAPI);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]       
        public async Task<ActionResult<ResponseAPI>> Get()
        {
            Log.Information("Request to receive all Pastes");

            IEnumerable<HomePasteDto> pastes = await _pasteService.GetAllPaste();
            FilterVM filter = await _filterService.GetAllFilters();

            if(pastes == null)
            {
                return NotFound();
            }
            else
            {
                var pasteVM = _mapper.Map<IEnumerable<HomePasteVM>>(pastes);
                _responseAPI.Data = new
                {
                    Pastes = pastes,
                    filter.Categories,
                    filter.ContentTypes,
                    filter.Languages
                };

                return Ok(_responseAPI);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(Guid id)
        {
            Log.Information("Request to receive Paste by id: {@id}", id);

            GetPasteDto pasteDto = await _pasteService.GetPasteById(id);

            if (pasteDto == null)
            {
                return NotFound();
            }
            else
            {
                var pasteVM = _mapper.Map<HomePasteVM>(pasteDto);
                _responseAPI.Data = pasteVM;

                return Ok(_responseAPI);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Update([FromBody] PasteDto pasteDto)
        {
            Log.Information("Request to edit a Paste with id: {@id}", pasteDto.Id);

            var valid = _validator.Validate(pasteDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _pasteService.UpdatePaste(pasteDto);

            if (!result)
            {
                return NotFound();
            }
            else
            {
                return Ok(_responseAPI);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI>> Delete(Guid id)
        {
            Log.Information("Request to delete a Paste with id: {@id}", id);

            bool result = await _pasteService.DeletePaste(id);

            if (!result)
            {
                return NotFound();
            }
            else
            {
                return Ok(_responseAPI);
            }
        }
    }
}
