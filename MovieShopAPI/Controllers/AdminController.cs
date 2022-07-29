using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieDetailsModel model)
        {
            var movie = await _adminService.CreateMovie(model);
            return Ok(movie);
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieDetailsModel model)
        {
            var movie = await _adminService.UpdateMovie(model);
            return Ok(movie);
        }

        [HttpGet]
        [Route("top-purhcased-movies")]
        public async Task<IActionResult> GetTopPurchasedMovies(DateTime? start, DateTime? end = null)
        {
            if (start == null)
            {
                start = DateTime.Today.AddDays(-90);
                end = DateTime.Today;
            }

            else if (end == null)
            {
                end = DateTime.Today;
            }

            var movies = await _adminService.GetMoviesPurchasedBetween((DateTime)start, (DateTime)end);

            return Ok(movies);
        }
    }
}
