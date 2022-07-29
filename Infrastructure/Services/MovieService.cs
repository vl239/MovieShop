using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	public class MovieService : IMovieService
	{
        public readonly IMovieRepository _movieRepository;
        //public readonly IRepository<Review> _reviewRepository;
        public readonly IPurchaseRepository _purchaseRepository;

        public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _movieRepository = movieRepository;
            //_reviewRepository = reviewRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int id)
        {
            var movieDetails = await _movieRepository.GetById(id);

            if (movieDetails == null)
            {
                return null;
            }

            int rating_count = movieDetails.Reviews.Select(r => r.Rating).Count();
            decimal rating = 0;
            if (rating_count != 0)
            {
                rating = movieDetails.Reviews.Select(r => r.Rating).Average();
            };            

            var movie = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Tagline = movieDetails.Tagline,
                Title = movieDetails.Title,
                Overview = movieDetails.Overview,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                ImdbUrl = movieDetails.ImdbUrl,
                RunTime = movieDetails.RunTime,
                TmdbUrl = movieDetails.TmdbUrl,
                Revenue = movieDetails.Revenue,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate,
                Rating = rating,
                Price = movieDetails.Price
            };

            foreach (var genre in movieDetails.GenresOfMovie)
            {
                movie.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            foreach (var trailer in movieDetails.Trailers)
            {
                movie.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl });
            }

            foreach (var cast in movieDetails.CastsOfMovie)
            {
                movie.Casts.Add(new CastModel { Id = cast.CastId, Name = cast.Cast.Name, Character = cast.Character, ProfilePath = cast.Cast.ProfilePath });
            }

            return movie;
        }

        public async Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies.PagedData)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return new PagedResultSetModel<MovieCardModel>(pageNumber, movies.TotalRecords, pageSize, movieCards);
        }

        // method that returns top movies to the caller
        // list of movies
        public async Task<List<MovieCardModel>> GetTopGrossingMovies()
        {
            var movies = await _movieRepository.Get30HighestGrossingMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return movieCards;
        }

        public async Task<List<MovieCardModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.Get30HighestRatedMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return movieCards;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int id)
        {
            var movie = await _movieRepository.GetById(id);
            return movie.Reviews;
        }

        //public async Task<List<PurchasedMoviesModel>> GetMoviesPurchasedBetween(DateTime start, DateTime end)
        //{
        //    var purchases = await _purchaseRepository.GetPurchasesBetween(start, end);
        //    var movieIds = purchases.Select(p => p.MovieId).Distinct();

        //    var movies = new List<PurchasedMoviesModel>();
        //    foreach (var movieId in movieIds)
        //    {
        //        int count = await _purchaseRepository.GetPurchaseCountForMovie(movieId);

        //        var movie = await _movieRepository.GetById(movieId);

        //        movies.Add(new PurchasedMoviesModel { Movie = movie, PurchaseCount = count });
        //    }

        //    return movies.OrderByDescending(m => m.PurchaseCount).ToList();
        //}
    }
}

