using System.Globalization;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories
{
    public class EfcUserRepository : IUserRepository
    {
        private readonly AppContex ctx;

        public EfcUserRepository(AppContex ctx)
        {
            this.ctx = ctx;
        }

        public async Task<User> AddAsync(User user)
        {
            EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task UpdateAsync(User user)
        {
            if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
            {
                throw new NotFoundException($"User with id {user.Id} not found");
            }

            ctx.Users.Update(user);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (existing == null)
            {
                throw new NotFoundException($"User with id {id} not found");
            }

            ctx.Users.Remove(existing);
            await ctx.SaveChangesAsync();
        }

        public IQueryable<User> GetMany()
        {
            return ctx.Users.AsQueryable();
        }

        public async Task<User> GetSingleByUsernameAsync(string username)
        {
            User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (existing == null)
            {
                throw new CultureNotFoundException("User not found");
            }
            return existing;
        }

        public async Task<User> GetSingleAsync(int id)
        {
            User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (existing == null)
            {
                throw new CultureNotFoundException("User not found");
            }
            return existing;
        }


    }
}
