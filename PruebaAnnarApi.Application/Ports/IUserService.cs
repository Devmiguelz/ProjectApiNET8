using PruebaAnnarApi.Application.Dto.User;
using PruebaAnnarApi.Application.Response;

namespace PruebaAnnarApi.Application.Ports
{ 
    public interface IUserService 
    {
        Task<Result<IEnumerable<UserListDto>>> GetAsync();
        Task<Result<UserListDto>> GetByIdAsync(Guid id);
        Task<Result<UserListDto>> AddAsync(UserCreateDto user);
        Task<Result<UserListDto>> UpdateAsync(UserUpdateDto user);
        Task<Result<bool>> DeleteAsync(Guid userId);
    } 
}  
