using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Movie> Get30HighestGrossingMovies()
        {
            // LINQ code to get top 30 grossing movies
            // select top 30 * from Movie order by Revenue
            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;
        }

        public IEnumerable<Movie> Get30HighestRatedMovies()
        {
            throw new NotImplementedException();
        }

        public override Movie GetById(int id)
        {
            // select * from Movie
            // join Cast and MovieCast
            // join Trailer
            // join Genre and MovieGenre
            // where Id = id
            // what do you use for join?? INCLUDE METHOD
            var movieDetails = _dbContext.Movies
                .Include(m => m.GenresOfMovie)
                .ThenInclude(m => m.Genre)
                .FirstOrDefault(m => m.Id == id);

            // FirstOrDefault
            // First => will throw an exception when there are no matching records

            // SingleOrDefault 0 or 1, ex => more than one matching record
            // Single => more than one matching record

            return movieDetails;
        }
    }
}

