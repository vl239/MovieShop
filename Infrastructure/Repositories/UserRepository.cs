using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> CheckIfMoviePurchasedByUser(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async override Task<User> GetById(int id)
        {
            var user = await _dbContext.Users
                //.Include(u => u.Favorites)
                //.Include(u => u.Purchases)
                //.Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}

