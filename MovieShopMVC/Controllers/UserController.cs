using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
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
        private readonly IUserService _userService;

        public UserController(ICurrentLoggedInUser currentLoggedInUser, IUserService userService)
        {
            _currentLoggedInUser = currentLoggedInUser;
            _userService = userService;
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
            var purchasedMovies = await _userService.GetAllPurchasesForUser(userId);
            // use the UserId and send to User Service to get information for that User 
            return View(purchasedMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentLoggedInUser.UserId;
            var favoriteMovies = await _userService.GetAllFavoritesForUser(userId);
            return View(favoriteMovies);
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

