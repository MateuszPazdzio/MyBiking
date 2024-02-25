using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using MyBiking.Application.Models;
using MyBiking.Entity.Models;

namespace MyBiking.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IMyBikingRepository myBikingRepository;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager , IMapper mapper, IMyBikingRepository myBikingRepository)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this.myBikingRepository = myBikingRepository;
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
        public async Task<IActionResult> Register()
        {
            var nationalities =await myBikingRepository.GetNationalities();
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
            return View(registerUserDto);
            //return Created($"Created user id: {result}", null);
        }
    }
}
