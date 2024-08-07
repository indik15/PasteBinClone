﻿using AutoMapper;
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
    [Authorize]
    [Route("api/userPaste")]
    [ApiController]
    public class UserPasteController(IPasteService pasteService, IMapper mapper) : ControllerBase
    {
        private readonly IPasteService _pasteService = pasteService;
        private readonly IMapper _mapper = mapper;
        private readonly ResponseAPI _responseAPI = new();

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseAPI>> Get([FromBody] int pageNumber, string userId)
        {
            Log.Information("Request to receive user Pastes by user id: {@id}", userId);

            (IEnumerable<HomePasteDto> pasteDto, int totalPages) = await _pasteService.GetAllUserPastes(userId, pageNumber);

            if (pasteDto == null)
            {
                return NotFound();
            }
            else
            {
                var pasteVM = _mapper.Map<IEnumerable<HomePasteVM>>(pasteDto);
                _responseAPI.Data = new
                {
                    TotalPages = totalPages,
                    Pastes = pasteVM
                };

                return Ok(_responseAPI);
            }
        }
    }
}
