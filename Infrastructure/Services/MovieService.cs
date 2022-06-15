using System;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	public class MovieService : IMovieService
	{
        public MovieDetailsModel GetMovieDetails(int id)
        {
            var movie = new MovieDetailsModel
            {

            };

            return movie;
        }

        // method that returns top movies to the caller
        // list of movies
        public List<MovieCardModel> GetTopGrossingMovies()
        {
            // call the movie repo to get the data from database
            var movies = new List<MovieCardModel>
            {
                new MovieCardModel { Id=1, PosterUrl="", Title="Inception"},
            new MovieCardModel { Id = 2, PosterUrl = "", Title = "" },
            new MovieCardModel { Id = 3, PosterUrl = "", Title = "" },
            new MovieCardModel { Id = 4, PosterUrl = "", Title = "" },
            new MovieCardModel { Id = 5, PosterUrl = "", Title = "" }
            };

            return movies;
        }
	}
}

