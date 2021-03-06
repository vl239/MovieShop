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
        public readonly IPurchaseRepository _purchaseRepository;

        public UserService(IUserRepository userRepository, IRepository<Favorite> favoriteRepository, IRepository<Review> reviewRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
            _purchaseRepository = purchaseRepository;
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
                if (favorite.Id == movieId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<MovieCardModel>> GetAllFavoritesForUser(int id)
        {
            var user = await _userRepository.GetById(id);
            var favorites = user.Favorites;
            var movieCards = new List<MovieCardModel>();

            foreach (var favorite in favorites)
            {
                movieCards.Add(new MovieCardModel { Id = favorite.MovieId, PosterUrl = favorite.Movie.PosterUrl, Title = favorite.Movie.Title });
            }
            return movieCards;
        }

        public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            var user = await _userRepository.GetById(id);
            var purchases = user.Purchases;
            var movieCards = new List<MovieCardModel>();

            foreach (var purchase in purchases)
            {
                movieCards.Add(new MovieCardModel { Id = purchase.MovieId, PosterUrl = purchase.Movie.PosterUrl, Title = purchase.Movie.Title });
            }
            return movieCards;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUser(int id)
        {
            var user = await _userRepository.GetById(id);
            return user.Reviews;
        }

        public async Task<Purchase> GetPurchasesDetails(int userId, int movieId)
        {
            var user = await _userRepository.GetById(userId);
            var purchases = user.Purchases;
            var purchaseDetails = purchases.FirstOrDefault(p => p.MovieId == movieId);
            return purchaseDetails;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchases = await GetAllPurchasesForUser(userId);
            foreach (var purchase in purchases)
            {
                if (purchase.Id == purchaseRequest.MovieId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            //if (await IsMoviePurchased(purchaseRequest, userId) == false)
            //{
                var moviePurchase = new Purchase
                {
                    Id = purchaseRequest.Id,
                    UserId = userId,
                    PurchaseNumber = purchaseRequest.PurchaseNumber,
                    TotalPrice = purchaseRequest.Price,
                    PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                    MovieId = purchaseRequest.MovieId
                };
                var moviePurchased = await _purchaseRepository.Add(moviePurchase);
            //    return true;
            //}
            //return false;
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var user = await _userRepository.GetById(favoriteRequest.UserId);
            var favorites = user.Favorites;
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
            var oldReview = await DeleteMovieReview(reviewRequest.UserId, reviewRequest.MovieId);
            await AddMovieReview(reviewRequest);
        }
    }
}

