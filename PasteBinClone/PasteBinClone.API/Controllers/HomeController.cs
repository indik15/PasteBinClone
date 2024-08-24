using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using Serilog;
using System.Collections.Generic;

namespace PasteBinClone.API.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController(IPasteService pasteService, IMapper mapper) : ControllerBase
    {
        private readonly IPasteService _pasteService = pasteService;
        private readonly IMapper _mapper = mapper;
        private readonly ResponseAPI _response = new();

        [HttpGet]
        public async Task<ActionResult<ResponseAPI>> Get()
        {
            Log.Information("Request to receive five popular posts");

            IEnumerable<HomePasteDto> pasteDtos = await _pasteService.GetTopRatedPastes();

            if(pasteDtos == null)
            {
                return NotFound();
            }

            _response.Data = _mapper.Map<IEnumerable<HomePasteVM>>(pasteDtos);

            return Ok(_response);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseAPI>> Get(string userId)
        {
            Log.Information("Request to receive five pastes of the current user");

            IEnumerable<HomePasteDto> pasteDtos = await _pasteService.GetFiveUserPastes(userId);

            if (pasteDtos == null)
            {
                return NotFound();
            }

            _response.Data = _mapper.Map<IEnumerable<HomePasteVM>>(pasteDtos);

            return Ok(_response);
        }
    }
}
