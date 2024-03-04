using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.User;
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
        private readonly IMediator _mediator;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , IMapper mapper, 
            IMyBikingRepository myBikingRepository, IMediator mediator)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._myBikingRepository = myBikingRepository;
            this._mediator = mediator;
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
        public async Task<IActionResult> Login(LoginUserDtoCommand loginUserDtoCommand)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result =await _mediator.Send(loginUserDtoCommand) as Status;
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
            //RegisterUserDto registerModelView = new RegisterUserDto();
            //registerModelView.Nationalities = await _myBikingRepository.GetNationalities();

            //return View(registerModelView);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserDtoCommand registerUserDtoCommand)
        {
            if (registerUserDtoCommand == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
            {
                //registerUserDto.Nationalities = await _myBikingRepository.GetNationalities();
                //return View(registerUserDto);               
                //registerUserDto.Nationalities = await _myBikingRepository.GetNationalities();
                return View(registerUserDtoCommand);
            }

            var result = await _mediator.Send(registerUserDtoCommand) as Status;

            if (result.StatusCode == 1)
            {
                return RedirectToAction("Login", "Auth");
            }
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
            //return Ok(result);
            //var nationalities = await _myBikingRepository.GetNationalities();
            //ViewData["Nationalities"] = nationalities;
            return View(registerUserDtoCommand);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._myBikingRepository.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
