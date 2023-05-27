using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class UserMappingService : IMappingService<User, UserDto>
    {
        public async Task<UserDto> MapEntityToDto(User User)
        {
            return new UserDto() { Email = User.Email, Password = User.PasswordHash };
        }

        public User MapDtoToEntity(UserDto UserDto)
        {
            return new User() {Email = UserDto.Email, PasswordHash = UserDto.Password};
        }
    }
}
