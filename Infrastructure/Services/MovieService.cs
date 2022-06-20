using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	public class MovieService : IMovieService
	{
        public readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public MovieDetailsModel GetMovieDetails(int id)
        {
            var movieDetails = _movieRepository.GetById(id);

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
                ReleaseDate = movieDetails.ReleaseDate
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

        // method that returns top movies to the caller
        // list of movies
        public List<MovieCardModel> GetTopGrossingMovies()
        {
            var movies = _movieRepository.Get30HighestGrossingMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return movieCards;
        }
	}
}

