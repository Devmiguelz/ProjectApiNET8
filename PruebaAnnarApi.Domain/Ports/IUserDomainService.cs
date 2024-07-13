using ModelWebApi.Domain.Entities;

namespace PruebaAnnarApi.Domain.Ports
{
    public interface IUserDomainService
    {
        Task<List<User>> GetAsync();
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid userId);
    }
}
