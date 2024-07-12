using PruebaAnnarApi.Application.Dto.User;

namespace PruebaAnnarApi.Application.Ports
{ 
    public interface IUserService 
    {
        Task<IEnumerable<UserListDto>> GetAsync();
        Task<UserListDto> GetByIdAsync(Guid id);
        Task AddAsync(UserCreateDto user);
        Task UpdateAsync(UserUpdateDto user);
        Task DeleteAsync(Guid userId);
    } 
}  
