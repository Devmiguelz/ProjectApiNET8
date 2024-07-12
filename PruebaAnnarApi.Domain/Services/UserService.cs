using ModelWebApi.Domain.Entities;
using PruebaAnnarApi.Domain.Interfaces;
using PruebaAnnarApi.Domain.Ports;

namespace PruebaAnnarApi.Domain.Services 
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAsync()
        {
            return await _userRepository.GetAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }
        
        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _userRepository.DeleteAsync(userId);
        }
    } 
} 
