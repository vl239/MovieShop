using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	public class AdminService : IAdminService
	{
        public readonly IMovieRepository _movieRepository;
        public readonly IPurchaseRepository _purchaseRepository;

        public AdminService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<List<PurchasedMoviesModel>> GetMoviesPurchasedBetween(DateTime start, DateTime end)
        {
            var purchases = await _purchaseRepository.GetPurchasesBetween(start, end);
            var movieIds = purchases.Select(p => p.MovieId).Distinct();

            var movies = new List<PurchasedMoviesModel>();
            foreach (var movieId in movieIds)
            {
                int count = await _purchaseRepository.GetPurchaseCountForMovie(movieId);

                var movie = await _movieRepository.GetById(movieId);

                movies.Add(new PurchasedMoviesModel { Movie = movie, PurchaseCount = count });
            }

            return movies.OrderByDescending(m => m.PurchaseCount).ToList();
        }

        public async Task<bool> CreateMovie(MovieDetailsModel model)
        {
            // check if movie already exists
            var movie = await _movieRepository.GetById(model.Id);
            if (movie != null)
            {
                throw new ConflictException("Movie already exists");
            }

            // create Movie object
            var newMovie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Budget = model.Budget,
                Revenue = model.Revenue,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                CreatedDate = DateTime.Now,
                //CreatedBy = creator.FirstName + " " + creator.LastName,
                GenresOfMovie = (ICollection<MovieGenre>)model.Genres
            };

            // save movie to Movie table
            var savedMovie = await _movieRepository.Add(newMovie);
            if (savedMovie.Id > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateMovie(MovieDetailsModel model)
        {
            var movie = await _movieRepository.GetById(model.Id);
            if (movie == null)
            {
                throw new ConflictException("Movie does not exist");
            }

            //movie.UpdatedBy = updater.FirstName + " " + updater.LastName;
            movie.UpdatedDate = DateTime.Now;

            await _movieRepository.Update(movie);
            return true;
        }
    }
}

