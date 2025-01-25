using App_web_book.Dtos;
using App_web_book.Entities;
using Microsoft.AspNetCore.Identity;

namespace App_web_book.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            });
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };
        }

        public async Task<IdentityResult> AddUserAsync(CreateUserDto newUser)
        {
            var user = new User
            {
                UserName = newUser.Email,
                Email = newUser.Email,
                FullName = newUser.FullName
            };

            return await _userManager.CreateAsync(user, newUser.Password);
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId, CreateUserDto userUpdates)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            user.FullName = userUpdates.FullName;
            user.Email = userUpdates.Email;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.DeleteAsync(user);
        }
    }
}
