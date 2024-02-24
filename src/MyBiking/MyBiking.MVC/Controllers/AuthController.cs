using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Models;

namespace MyBiking.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager , IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(LoginUserDto loginUserDto)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                return View(registerUserDto);
            }
            var user = _mapper.Map<User>(registerUserDto);

            var result =await _userManager.CreateAsync(user, user.PasswordHelpers.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index","Home");
            }
            //int result = _userService.Register(registerUserDto);
            //if (result == 0)
            //{
            //    return BadRequest();
            //}
            return null;
            //return Created($"Created user id: {result}", null);
        }
    }
}
