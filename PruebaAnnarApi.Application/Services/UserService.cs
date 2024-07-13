using AutoMapper;
using ModelWebApi.Domain.Entities;
using PruebaAnnarApi.Application.Dto.User;
using PruebaAnnarApi.Application.Exceptions;
using PruebaAnnarApi.Application.Ports;
using PruebaAnnarApi.Domain.Ports;

namespace PruebaAnnarApi.Application.Services 
{ 
    public class UserService: IUserService 
    { 
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper; 
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper; 
        }

        public async Task<IEnumerable<UserListDto>> GetAsync()
        {
            try
            {
                List<User> userList = await _unitOfWork.UserRepository.GetAsync();
                if (!userList.Any())
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
                User userData = await _unitOfWork.UserRepository.GetByIdAsync(id);
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
                await _unitOfWork.UserRepository.AddAsync(userNew);
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
                await _unitOfWork.UserRepository.UpdateAsync(userUpdate);
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
                await _unitOfWork.UserRepository.DeleteAsync(userId);    
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }
    } 
} 
