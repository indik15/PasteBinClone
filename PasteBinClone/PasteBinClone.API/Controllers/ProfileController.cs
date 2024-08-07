using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using Serilog;
using System.Collections.Generic;

namespace PasteBinClone.API.Controllers
{
    [Route("api/profile")]
    [Authorize]
    [ApiController]
    public class ProfileController(IPasteService pasteService, 
        IMapper mapper,
        IApiUserService userService) : ControllerBase
    {
        private readonly IPasteService _pasteService = pasteService;
        private readonly IMapper _mapper = mapper;
        private readonly IApiUserService _userService = userService;
        private readonly ResponseAPI _responseAPI = new();

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseAPI>> Get(string userId)
        {
            Log.Information("Request to receive user profile by user id: {@id}", userId);

            IEnumerable<HomePasteDto> pastes = await _pasteService.GetFiveUserPastes(userId);
            ApiUser apiUser = await _userService.GetApiUserById(userId);

            if (apiUser == null)
            {
                return NotFound();
            }
            else
            {
                var pasteVM = _mapper.Map<IEnumerable<HomePasteVM>>(pastes);
                var userVM = _mapper.Map<ApiUserVM>(apiUser);

                _responseAPI.Data = new
                {
                    Pastes = pasteVM,
                    ApiUser = userVM
                };

                return Ok(_responseAPI);
            }
        }
    }
}
