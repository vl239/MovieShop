using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IMovieService
	{
		// all the business functionality methods pertaining to movies
		Task<List<MovieCardModel>> GetTopGrossingMovies();

		// get movie details
		Task<MovieDetailsModel> GetMovieDetails(int id);
	}
}

