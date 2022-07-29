using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IAdminService
	{
		Task<List<PurchasedMoviesModel>> GetMoviesPurchasedBetween(DateTime start, DateTime end);

		Task<bool> CreateMovie(MovieDetailsModel model);

		Task<bool> UpdateMovie(MovieDetailsModel model);
	}
}

