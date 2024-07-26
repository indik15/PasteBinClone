using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/rating")]
    public class RatingController(IRatingService ratingService) : ControllerBase
    {
        private readonly IRatingService _ratingService = ratingService;
        private readonly ResponseAPI response = new();

        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] RatingDto ratingDto)
        {
            bool result = await _ratingService.UpdatePasteRating(ratingDto);

            if (result)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
