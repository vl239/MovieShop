using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTopGrossingMovies();

            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for id: {id}" });
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();

            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("top-grossing")]
        // attribute routing
        // MVC http://localhost/movies/GetTopGrossingMovies => traditional/conventional based routing
        // http://localhost/api/movies/top-grossing
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            // call my service
            var movies = await _movieService.GetTopGrossingMovies();

            // return the movies info in JSON format
            // ASP.NET Core automatically serializes c# objects to JSON objects
            // System.Text.Json library for .NET 3 and newer
            // older versions of .NET used Newtonsoft.Json
            // return data in json format and the HTTP status code
            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies); 
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);

            if (movies == null)
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id)
        {
            var reviews = await _movieService.GetMovieReviews(id);

            if (reviews == null || !reviews.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Reviews Found For This Movie" });
            }

            return Ok(reviews);

        }
    }
}
