using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetUserDetails()
        {
            return Ok();
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie()
        {
            return Ok();
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite()
        {
            return Ok();
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> DeleteFavorite()
        {
            return Ok();
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> CheckMovieFavorite(int movieId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview()
        {
            return Ok();
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> UpdateReview()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("delete-review/{movieId:int}")]
        public async Task<IActionResult> DeleteReview(int movieId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetPurchases()
        {
            // get the userId from the token, using HttpContext

            return Ok();
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> GetPurchaseDetails(int movieId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> CheckMoviePurchased(int movieId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            return Ok();
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> GetMovieReviews()
        {
            return Ok();
        }
    }
}
