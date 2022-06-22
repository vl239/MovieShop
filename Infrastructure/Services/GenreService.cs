using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
	public class GenreService : IGenreService
	{
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();

            var genresModel = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name });

            return genresModel;
        }

        public async Task<List<MovieCardModel>> GetMoviesByGenre(int id)
        {
            var moviesOfGenre = await _genreRepository.GetMoviesOfGenre(id);

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in moviesOfGenre)
            {
                movieCards.Add(new MovieCardModel { Id = movie.MovieId, PosterUrl = movie.Movie.PosterUrl, Title = movie.Movie.Title });
            }

            return movieCards;
        }
    }
}

