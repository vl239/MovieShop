using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IRepository<Favorite> _favoriteRepository;
        public readonly IRepository<Review> _reviewRepository;

        public UserService(IUserRepository userRepository, IRepository<Favorite> favoriteRepository, IRepository<Review> reviewRepository)
        {
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }


        public async Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            if (await FavoriteExists(favoriteRequest.UserId, favoriteRequest.MovieId) == false)
            {
                var newFavorite = new Favorite
                {
                    Id = favoriteRequest.Id,
                    MovieId = favoriteRequest.MovieId,
                    UserId = favoriteRequest.UserId
                };

                var savedFavorite = await _favoriteRepository.Add(newFavorite);
                return true;
            }
            return false;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var newReview = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };

            var savedReview = await _reviewRepository.Add(newReview);
        }

        public async Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            var reviews = await GetAllReviewsByUser(userId);
            var deleteReview = reviews.FirstOrDefault(r => r.MovieId == movieId);
            if (deleteReview != null)
            {
                var deletedReview = await _reviewRepository.Delete(deleteReview);
                return true;
            }
            return false;
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var favorites = await GetAllFavoritesForUser(id);
            foreach (var favorite in favorites)
            {
                if (favorite.UserId == id && favorite.MovieId == movieId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesForUser(int id)
        {
            var user = await _userRepository.GetById(id);

            return user.Favorites;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int id)
        {
            var user = await _userRepository.GetById(id);

            return user.Purchases;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUser(int id)
        {
            var user = await _userRepository.GetById(id);

            return user.Reviews;
        }

        public async Task GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchases = await GetAllPurchasesForUser(userId);
            foreach (var purchase in purchases)
            {
                if (purchase.Id == purchaseRequest.Id)
                {
                    return true;
                }
            }
            return false;

        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorites = await GetAllFavoritesForUser(favoriteRequest.UserId);
            var removeFavorite = favorites.FirstOrDefault(f => f.MovieId == favoriteRequest.MovieId);
            if (removeFavorite != null)
            {
                var removedFavorite = await _favoriteRepository.Delete(removeFavorite);
                return true;
            }
            return false;
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }
    }
}

