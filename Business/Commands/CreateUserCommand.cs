using UserManagement.Api.Models.DTOs;

namespace UserManagement.Api.Business.Commands
{
    public class CreateUserCommand
    {
        public CreateUserDto UserDto { get; set; }

        public CreateUserCommand(CreateUserDto userDto)
        {
            UserDto = userDto;
        }
    }
}