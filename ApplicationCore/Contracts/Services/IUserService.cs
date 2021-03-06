using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IUserService
	{
		Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);

		Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);

		Task<List<MovieCardModel>> GetAllPurchasesForUser(int id);

		Task<Purchase> GetPurchasesDetails(int userId, int movieId);

		Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest);

		Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest);

		Task<bool> FavoriteExists(int id, int movieId);

		Task<List<MovieCardModel>> GetAllFavoritesForUser(int id);

		Task AddMovieReview(ReviewRequestModel reviewRequest);

		Task UpdateMovieReview(ReviewRequestModel reviewRequest);

		Task<bool> DeleteMovieReview(int userId, int movieId);

		Task<IEnumerable<Review>> GetAllReviewsByUser(int id);
	}
}

