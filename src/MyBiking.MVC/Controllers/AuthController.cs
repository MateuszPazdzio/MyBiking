using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.User;
using MyBiking.Entity.IRepository;
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
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IUserRepository _myBikingRepository;
        private readonly IMediator _mediator;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , IMapper mapper,
            IUserRepository myBikingRepository, IMediator mediator)
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
            if(loginUserDtoCommand == null)
            {
                return BadRequest();
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _mediator.Send(loginUserDtoCommand) as Status;
                if (result.StatusCode == 1)
                {
                    return RedirectToAction("Index", "Home");
                }

                TempData["msg"] = result.Message;

                return View(loginUserDtoCommand);
            }
            catch (Exception)
            {
                return View(loginUserDtoCommand);
                throw;
            }
            
            
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserDtoCommand registerUserDtoCommand)
        {
            if (registerUserDtoCommand == null)
            {
                return BadRequest();
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(registerUserDtoCommand);
                }

                var result = await _mediator.Send(registerUserDtoCommand);

                if (result.Code == Entity.Enums.Code.HTTP201)
                {
                    return RedirectToAction("Login", "Auth");
                }

                return View(registerUserDtoCommand);

            }
            catch (Exception)
            {
                return View(registerUserDtoCommand);
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._myBikingRepository.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
