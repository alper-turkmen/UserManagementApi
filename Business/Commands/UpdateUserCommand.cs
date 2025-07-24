using UserManagement.Api.Models.DTOs;

namespace UserManagement.Api.Business.Commands
{
    public class UpdateUserCommand
    {
        public int Id { get; set; }
        public UpdateUserDto UserDto { get; set; }

        public UpdateUserCommand(int id, UpdateUserDto userDto)
        {
            Id = id;
            UserDto = userDto;
        }
    }
}