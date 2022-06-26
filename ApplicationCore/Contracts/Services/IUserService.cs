﻿using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IUserService
	{
		Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);

		Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);

		Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int id);

		Task GetPurchasesDetails(int userId, int movieId);

		Task AddFavorite(FavoriteRequestModel favoriteRequest);

		Task RemoveFavorite(FavoriteRequestModel favoriteRequest);

		Task<bool> FavoriteExists(int id, int movieId);

		Task<IEnumerable<Favorite>> GetAllFavoritesForUser(int id);

		Task AddMovieReview(ReviewRequestModel reviewRequest);

		Task UpdateMovieReview(ReviewRequestModel reviewRequest);

		Task DeleteMovieReview(int userId, int movieId);

		Task<IEnumerable<Review>> GetAllReviewsByUser(int id);
	}
}

