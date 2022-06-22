using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IGenreService
	{
		Task<IEnumerable<GenreModel>> GetAllGenres();

		Task<List<MovieCardModel>> GetMoviesByGenre(int id);
	}
}

