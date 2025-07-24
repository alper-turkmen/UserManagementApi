using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Business.Commands;
using UserManagement.Api.Business.Handlers;
using UserManagement.Api.Business.Queries;
using UserManagement.Api.Models.DTOs;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserCommandHandler _commandHandler;
        private readonly UserQueryHandler _queryHandler;

        public UsersController(UserCommandHandler commandHandler, UserQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllUsersQuery(page, pageSize);
            var result = await _queryHandler.HandleAsync(query);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _queryHandler.HandleAsync(query);

            if (result.IsSuccess)
                return Ok(result);

            if (result.Message.Contains("bulunamadı"))
                return NotFound(result);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { IsSuccess = false, Message = "Validasyon hatası", Errors = errors });
            }

            var command = new CreateUserCommand(createUserDto);
            var result = await _commandHandler.HandleAsync(command);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetUserById), new { id = result.Data!.Id }, result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { IsSuccess = false, Message = "Validasyon hatası", Errors = errors });
            }

            var command = new UpdateUserCommand(id, updateUserDto);
            var result = await _commandHandler.HandleAsync(command);

            if (result.IsSuccess)
                return Ok(result);

            if (result.Message.Contains("bulunamadı"))
                return NotFound(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _commandHandler.HandleAsync(command);

            if (result.IsSuccess)
                return Ok(result);

            if (result.Message.Contains("bulunamadı"))
                return NotFound(result);

            return BadRequest(result);
        }
    }
}