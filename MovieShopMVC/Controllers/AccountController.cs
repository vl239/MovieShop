using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;


namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var isValidPassword = await _accountService.ValidateUser(model.Email, model.Password);
            if (isValidPassword == true)
            {
                //redirect to home page
                return LocalRedirect("~/");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // show the empty register page when we make a GET request
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            // model binding (case insensitive)
            // NAMES are posted
            var user = await _accountService.RegisterUser(model);
            // redirect to login page
            return RedirectToAction("Login");
        }
    }
}

