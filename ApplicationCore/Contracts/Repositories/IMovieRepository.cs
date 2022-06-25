using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IMovieRepository : IRepository<Movie>
	{
		Task<IEnumerable<Movie>> Get30HighestGrossingMovies();

		Task<IEnumerable<Movie>> Get30HighestRatedMovies();

		Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);

	}
}

