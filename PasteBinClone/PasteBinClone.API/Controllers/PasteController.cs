using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

            Guid result = await _pasteService.CreatePaste(pasteDto);

            if (result != Guid.Empty)
            {
                _responseAPI.Data = result;
                return Ok(_responseAPI);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]       
        public async Task<ActionResult<ResponseAPI>> Get([FromQuery] HomePasteRequestDto homePasteRequestDto)
        {
            Log.Information("Request to receive all Pastes");

            (IEnumerable<HomePasteDto> pastes, int totalPages) = await _pasteService.GetAllPaste(homePasteRequestDto);

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
                    TotalPages = totalPages
                };

                return Ok(_responseAPI);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(Guid id, [FromQuery] string password = null, [FromQuery] string userId = null)
        {
            Log.Information("Request to receive Paste by id: {@id}", id);

            (GetPasteDto pasteDto, string validationError ) = await _pasteService.GetPasteById(id, userId, password);

            if (pasteDto == null)
            {
                return NotFound();
            }           
            else
            {
                if (!string.IsNullOrEmpty(validationError))
                {
                    _responseAPI.Errors.Add(validationError);
                }

                var pasteVM = _mapper.Map<GetPasteVM>(pasteDto);
                _responseAPI.Data = pasteVM;

                return Ok(_responseAPI);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] PasteDto pasteDto)
        {
            Log.Information("Request to edit a Paste with id: {@id}", pasteDto.Id);

            var valid = _validator.Validate(pasteDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _pasteService.UpdatePaste(pasteDto);

            if (result)
            {
                return Ok(_responseAPI);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI>> Delete(Guid id)
        {
            Log.Information("Request to delete a Paste with id: {@id}", id);

            bool result = await _pasteService.DeletePaste(id);

            if (result)
            {
                return Ok(_responseAPI);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
