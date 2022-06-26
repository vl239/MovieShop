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

        public UserService(IUserRepository userRepository, IRepository<Favorite> favoriteRepository)
        {
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
        }


        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
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

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }
    }
}

