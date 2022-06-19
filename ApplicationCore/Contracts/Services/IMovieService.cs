using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IMovieService
	{
		// all the business functionality methods pertaining to movies
		List<MovieCardModel> GetTopGrossingMovies();

		// get movie details
		MovieDetailsModel GetMovieDetails(int id);
	}
}

