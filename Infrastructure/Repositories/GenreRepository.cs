using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class GenreRepository : Repository<Genre>, IGenreRepository
	{
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async override Task<Genre> GetById(int id)
        {
            var genre = await _dbContext.Genres
                .Include(g => g.MoviesOfGenre)
                .ThenInclude(g => g.Movie)
                .FirstOrDefaultAsync(g => g.Id == id);

            return genre;
        }

        public async Task<IEnumerable<MovieGenre>> GetMoviesOfGenre(int id)
        {
            var genre = await GetById(id);

            var movies = genre.MoviesOfGenre.ToList();

            return movies;
        }
	}
}

