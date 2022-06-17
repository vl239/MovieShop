using System;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories
{
	public interface IMovieRepository : IRepository<Movie>
	{
		IEnumerable<Movie> Get30HighestGrossingMovies();

		IEnumerable<Movie> Get30HighestRatedMovies();
	}
}

