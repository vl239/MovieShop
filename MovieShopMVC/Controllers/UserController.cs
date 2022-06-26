using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;


namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // all these action methods should only be executed when user is logged in

        private readonly ICurrentLoggedInUser _currentLoggedInUser;

        public UserController(ICurrentLoggedInUser currentLoggedInUser)
        {
            _currentLoggedInUser = currentLoggedInUser;
        }

        [HttpGet]
        // write a code that will check if user is authenticated
        // *** FILTERS *** [Authorize]
        public async Task<IActionResult> Purchases()
        {
            // go to database and get all the movies purchased by user, user id
            // var cookie = this.HttpContext.Request.Cookies["MovieShopAuthCookie"];
            // can create a class that exposes HttpContext cookie decrypted info and claims
            // HttpContext decrypts claims automatically
            var userId = _currentLoggedInUser.UserId;
            // use the UserId and send to User Service to get information for that User 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            return View();
        }

    }
}

