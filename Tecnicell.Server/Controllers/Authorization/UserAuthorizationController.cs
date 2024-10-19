
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Tecnicell.Server.Models.Request;
using Tecnicell.Server.Models.Responses.Authorization;
using Tecnicell.Server.Services;

namespace Tecnicell.Server.Context.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthorizationController : ControllerBase
    {
        private readonly TecnicellContext _context;
        private IUserService _userService;

        public UserAuthorizationController(TecnicellContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/ApiUseraccounts/5
        [HttpPost("login")]
        public IActionResult GetUseraccount([FromBody] AuthRequest user)
        {
            Response response = new Response();
            if(user == null) { return NotFound(); }

            try
            {
                var res = _userService.Auth(_context, user);

                response.Success = 1;
                response.Message = "Success";
                response.user = res;

                return Ok(response);
            }
            catch (Exception)
            {
                response.Success = 0;
                response.Message = "Bad user name or password";
                return BadRequest(response);
            }
        }
    }
}
