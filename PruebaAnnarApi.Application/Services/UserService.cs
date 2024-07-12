using AutoMapper;
using ModelWebApi.Domain.Entities;
using PruebaAnnarApi.Application.Dto.User;
using PruebaAnnarApi.Application.Exceptions;
using PruebaAnnarApi.Application.Ports;
using PruebaAnnarApi.Domain.Interfaces;

namespace PruebaAnnarApi.Application.Services 
{ 
    public class UserService: IUserService 
    { 
        private readonly IUserRepository _userRepository; 
        private readonly IMapper _mapper; 
        public UserService(IUserRepository userDomain, IMapper mapper) 
        {
            _userRepository = userDomain; 
            _mapper = mapper; 
        }

        public async Task<IEnumerable<UserListDto>> GetAsync()
        {
            try
            {
                List<User> userList = await _userRepository.GetAsync();
                if (userList.Any())
                {
                    throw new NotFoundException("Users not found.");
                }
                return _mapper.Map<IEnumerable<User>, IEnumerable<UserListDto>>(userList);
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        public async Task<UserListDto> GetByIdAsync(Guid id)
        {
            try
            {
                User userData = await _userRepository.GetByIdAsync(id);
                if (userData is null)
                {
                    throw new NotFoundException("User not found.");
                }
                return _mapper.Map<User, UserListDto>(userData);
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        public async Task AddAsync(UserCreateDto user)
        {
            try
            {
                User userNew = _mapper.Map<UserCreateDto, User>(user);
                await _userRepository.AddAsync(userNew);
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }            
        }        
               
        public async Task UpdateAsync(UserUpdateDto user)
        {
            try
            {
                User userUpdate = _mapper.Map<UserUpdateDto, User>(user);
                await _userRepository.UpdateAsync(userUpdate);
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }            
        }

        public async Task DeleteAsync(Guid userId)
        {
            try
            {
                await _userRepository.DeleteAsync(userId);    
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }
    } 
} 
