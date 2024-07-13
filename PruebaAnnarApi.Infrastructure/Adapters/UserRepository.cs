using Microsoft.EntityFrameworkCore;
using ModelWebApi.Domain.Entities;
using PruebaAnnarApi.Domain.Interfaces;
using PruebaAnnarApi.Infrastructure.Persistence;
using System.Data;

namespace PruebaAnnarApi.Infrastructure.Adapters
{ 
    public class UserRepository: IUserRepository 
    {
        private readonly DataContext _context; 

        public UserRepository(DataContext dataContext) 
        { 
            _context = dataContext;
        }

        public async Task<List<User>> GetAsync()
        {
            List<User> users = await _context.User.Where(x => x.DeletedAt == null).ToListAsync();
            if (!users.Any())
            {
                throw new InvalidOperationException("User not found.");
            }
            return users;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            User? userExist = await _context.User.FindAsync(id);
            if (userExist is null)
            {
                throw new InvalidOperationException("User not found.");
            }
            return userExist;
        }

        public async Task AddAsync(User user)
        {
            user.Id = Guid.NewGuid();   
            user.CreatedAt = DateTime.Now;
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var userExist = await _context.User.FindAsync(user.Id);
            if (userExist is null)
            {
                throw new InvalidOperationException("User not found.");
            }
            user.CreatedAt = userExist.CreatedAt;
            user.UpdatedAt = DateTime.Now;
            _context.Entry(userExist).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var userExist = await _context.User.FindAsync(userId);
            if (userExist is null)
            {
                throw new InvalidOperationException("User not found.");
            }
            userExist.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    } 
} 
