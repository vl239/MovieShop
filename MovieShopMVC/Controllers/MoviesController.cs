﻿using System;
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
        //private readonly IGenreService _genreService;

        //public MoviesController(IMovieService movieService, IGenreService genreService)
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
            //_genreService = genreService;
        }

        // showing details of the movie
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            return View(movie);
        }

        public async Task<IActionResult> Genre(int id, int pageSize = 30, int pageNumber = 1)
        {
            // call movie Service and get the data
            var pagedMovies = await _movieService.GetMoviesByGenre(id, pageSize, pageNumber);
            return View("PagedMovies", pagedMovies);
        }
    }
}

