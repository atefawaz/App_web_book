using App_web_book.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App_web_book.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<IdentityResult> AddUserAsync(CreateUserDto newUser);
        Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto userUpdates);
        Task<IdentityResult> DeleteUserAsync(string userId);
    }
}