using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IMovieService
	{
		// all the business functionality methods pertaining to movies
		Task<List<MovieCardModel>> GetTopGrossingMovies();

		Task<List<MovieCardModel>> GetTopRatedMovies();

		// get movie details
		Task<MovieDetailsModel> GetMovieDetails(int id);

		Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);

		Task<IEnumerable<Review>> GetMovieReviews(int id);

		//Task<List<PurchasedMoviesModel>> GetMoviesPurchasedBetween(DateTime start, DateTime end);
	}
}

