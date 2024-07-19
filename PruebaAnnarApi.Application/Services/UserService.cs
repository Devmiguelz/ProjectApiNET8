using AutoMapper;
using ModelWebApi.Domain.Entities;
using PruebaAnnarApi.Application.Dto.User;
using PruebaAnnarApi.Application.Exceptions;
using PruebaAnnarApi.Application.Ports;
using PruebaAnnarApi.Application.Response;
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

        public async Task<Result<IEnumerable<UserListDto>>> GetAsync()
        {
            try
            {
                List<User> userList = await _unitOfWork.UserRepository.GetAsync();
                if (!userList.Any())
                {
                    return Result<IEnumerable<UserListDto>>.Failure([ErrorMessages.UserNotFound]);
                }

                var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserListDto>>(userList);

                return Result<IEnumerable<UserListDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserListDto>>.Failure([$"{ErrorMessages.GenericError} { ex.Message }"]);
            }
        }

        public async Task<Result<UserListDto>> GetByIdAsync(Guid id)
        {
            try
            {
                User userData = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (userData is null)
                {
                    return Result<UserListDto>.Failure([ErrorMessages.UserNotFound]);
                }
                var user = _mapper.Map<User, UserListDto>(userData);
                return Result<UserListDto>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<UserListDto>.Failure([$"{ErrorMessages.GenericError} {ex.Message}"]);
            }
        }

        public async Task<Result<UserListDto>> AddAsync(UserCreateDto user)
        {
            try
            {
                User userNew = _mapper.Map<UserCreateDto, User>(user);
                await _unitOfWork.UserRepository.AddAsync(userNew);
                await _unitOfWork.SaveAsync();
                return Result<UserListDto>.Success(_mapper.Map<UserListDto>(userNew));
            }
            catch (Exception ex)
            {
                return Result<UserListDto>.Failure([$"{ErrorMessages.GenericError} {ex.Message}"]);
            }            
        }        
               
        public async Task<Result<UserListDto>> UpdateAsync(UserUpdateDto user)
        {
            try
            {
                User userUpdate = _mapper.Map<UserUpdateDto, User>(user);
                await _unitOfWork.UserRepository.UpdateAsync(userUpdate);
                await _unitOfWork.SaveAsync();
                return Result<UserListDto>.Success(_mapper.Map<UserListDto>(userUpdate));
            }
            catch (Exception ex)
            {
                return Result<UserListDto>.Failure([$"{ErrorMessages.GenericError} {ex.Message}"]);
            }            
        }

        public async Task<Result<bool>> DeleteAsync(Guid userId)
        {
            try
            {
                await _unitOfWork.UserRepository.DeleteAsync(userId);
                await _unitOfWork.SaveAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure([$"{ErrorMessages.GenericError} {ex.Message}"]);
            }
        }
    } 
} 
