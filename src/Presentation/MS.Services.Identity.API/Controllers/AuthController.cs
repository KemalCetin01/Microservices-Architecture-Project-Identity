using Microsoft.AspNetCore.Mvc;
using MS.Services.Core.Base.Api;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Handlers.Auth.Queries;

namespace MS.Services.Identity.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IRequestBus _requestBus;
        public AuthController(IRequestBus requestBus)
        {
            _requestBus = requestBus;
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> Getuserinfo([FromHeader] string token)
      => Ok(await _requestBus.Send(new GetUserInfoQuery() { Token = token }));
    }
}
