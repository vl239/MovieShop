using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;


namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public MoviesController(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        // showing details of the movie
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            return View(movie);
        }

        public async Task<IActionResult> MoviesByGenre(int id, int pageSize = 30, int pageNumber = 1)
        {
            var ResultSet = await _genreService.GetMoviesByGenre(id);
            var PaginatedResultSet = ResultSet.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return View(PaginatedResultSet);
        }
    }
}

