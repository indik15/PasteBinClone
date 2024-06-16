using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.Services;

namespace PasteBinClone.API.Controllers
{
    [Route("api/filters")]
    [ApiController]
    public class FilterController(IFilterService filterService) : ControllerBase
    {
        private readonly IFilterService _filterService = filterService;
        private readonly ResponseAPI _responseAPI = new();

        [HttpGet]
        public async Task<ActionResult<ResponseAPI>> Get()
        {
            var result = await _filterService.GetAllFilters();
            _responseAPI.Data = result;

            return Ok(_responseAPI);
        }
    }
}
