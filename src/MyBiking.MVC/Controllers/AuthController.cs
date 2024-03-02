using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using MyBiking.Application.Models;
using MyBiking.Entity.Models;
using MyBiking.Infrastructure.Repository;
using System.Security.Claims;

namespace MyBiking.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IMyBikingRepository _myBikingRepository;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , IMapper mapper, IMyBikingRepository myBikingRepository)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._myBikingRepository = myBikingRepository;
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = _mapper.Map<ApplicationUser>(loginUserDto);

            var result =await _myBikingRepository.LoginUser(user);
            if(result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["msg"] = result.Message;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var nationalities = await _myBikingRepository.GetNationalities();
            ViewData["Nationalities"] = nationalities;

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

            var user = _mapper.Map<ApplicationUser>(registerUserDto);
            var result =await _myBikingRepository.CreateUser(user);

            //if (result.Succeeded)
            //{
            //    if(User.Identity.IsAuthenticated)
            //    {
            //        await Console.Out.WriteLineAsync("");
            //    }
            //    return RedirectToAction("Index","Home");
            //}

            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError("", error.Description);
            //}
            return Ok(result);
            //var nationalities = await _myBikingRepository.GetNationalities();
            //ViewData["Nationalities"] = nationalities;
            //return View(registerUserDto);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._myBikingRepository.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
