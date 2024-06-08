using PasteBinClone.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.Application.Interfaces;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
    [Route("api/receptionUser")]
    public class ReceptionUserController(IConfiguration configuration,
        IApiUserService userService) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IApiUserService _userService = userService;

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ApiUserViewModel apiUser)
        {
            if(apiUser.ApiCode != _configuration["IdentityUser:SecretKey"])
            {
                return NotFound();
            }

            bool result = await _userService.CreateUser(apiUser.ApiUser);

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
