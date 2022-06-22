using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;


namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            return View();
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
            return View();
        }
    }
}

