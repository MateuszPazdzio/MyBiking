using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.User;
using MyBiking.Application.Functions.Command.User.Api;

namespace MyBiking.MVC.Controllers
{
    public class AuthApiController : Controller
    {
        private readonly IMediator _mediator;

        public AuthApiController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDtoApiCommand loginUserDtoApi)
        {
            var result = await _mediator.Send(loginUserDtoApi);
            return Ok(result);
        }
    }
}
