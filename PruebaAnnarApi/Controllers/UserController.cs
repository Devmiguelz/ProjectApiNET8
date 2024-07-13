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
        public async Task<ActionResult<IEnumerable<UserListDto>>> Get()
        {
            var user = await _userService.GetAsync();
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<ActionResult<UserListDto>> Get([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add([FromBody] UserCreateDto user)
        {
            await _userService.AddAsync(user);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> Update([FromBody] UserUpdateDto user)
        {
            await _userService.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }

    } 
} 
