using PasteBinClone.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.Application.ViewModels;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
    [Route("api/receptionUser")]
    public class ReceptionUserController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> GetUser([FromBody] ApiUserViewModel apiUser)
        {
            return Ok(apiUser);
        }
    }
}
