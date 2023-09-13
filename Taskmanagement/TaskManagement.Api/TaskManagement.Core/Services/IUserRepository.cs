using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Services
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(UserDto userDto);
        Task<UserDto> ReadUserAsync(string userId);
        Task<IEnumerable<UserDto>> ReadAllUsersAsync();
        Task<bool> UpdateUserDetailsAsync(string userId, UserDto updatedUserDto);
        Task<bool> DeleteUserByIdAsync(string userId);
    }
}
