using Microsoft.AspNetCore.Mvc;
using PruebaAnnarApi.Application.Dto.User;
using PruebaAnnarApi.Application.Ports;

namespace PruebaAnnarApi.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class UserController : ControllerBase 
    { 
        private readonly IUserService _userService; 

        public UserController(IUserService userService) 
        { 
            _userService = userService; 
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAsync();
            return result.Match<IActionResult>(
                    onSuccess: user => Ok(result), 
                    onFailure: errors => NotFound(result)
                );
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return result.Match<IActionResult>(
                    onSuccess: user => Ok(result),
                    onFailure: errors => NotFound(result)
                );
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] UserCreateDto user)
        {
            var result = await _userService.AddAsync(user);
            return result.Match<IActionResult>(
                    onSuccess: user => Ok(result),
                    onFailure: errors => NotFound(result)
                );
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto user)
        {
            var result = await _userService.UpdateAsync(user);
            return result.Match<IActionResult>(
                    onSuccess: user => Ok(result),
                    onFailure: errors => NotFound(result)
                );
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            return result.Match<IActionResult>(
                    onSuccess: user => NoContent(),
                    onFailure: errors => NotFound(result)
                );
        }

    } 
} 
