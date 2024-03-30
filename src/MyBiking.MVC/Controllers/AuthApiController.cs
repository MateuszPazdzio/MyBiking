using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;

namespace MyBiking.MVC.Controllers
{
    public class AuthApiController : Controller
    {

        public IActionResult Login([FromBody] LoginUserDtoApi loginUserDtoApi)
        {

            return View();
        }
    }
}
