using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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
            var user = await _accountService.ValidateUser(model.Email, model.Password);
            if (user != null)
            {
                //redirect to home page
                // create a cookie, cookies are always sent from browser automatically to server
                // inside the cookie we store encrypted info (User claims) that server can recognize and tell whether user is logged in or not
                // Cookies should have an expiration time (ex: 2 hours)

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Surname, ""),
                    new Claim(ClaimTypes.GivenName, ""),
                    new Claim(ClaimTypes.NameIdentifier, ""),
                    new Claim(ClaimTypes.DateOfBirth, ""),
                    new Claim(ClaimTypes.Country, "USA"),
                    new Claim("Language", "English"),

                };

                // create cookie and cookie will have the above claims information along with expiration time
                // dont store above information in the cookie as plain text, instead encrypt the above information

                // we need to tell ASP.NET application that we are using Cookie based Authentication so that ASP.NET can generate Cookie based on the settings we provide

                // identity object that will identify the suer with claims
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // principal object which is used by ASP.NET to recognize the User
                // create the Cookie with above info

                // ***HttpContext*** THE MOST IMPORTANT CLASS
                // encapsulates your Http Request info (request from browser to server, lots of info
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

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

