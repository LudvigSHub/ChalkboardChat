using ChalkboardChat.BLL.DTOs;
using ChalkboardChat.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ChangeUsernameAsync(UpdateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.NewUsername))
                throw new ArgumentException("Username cannot be empty");

            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user is null)
                throw new InvalidOperationException("User not found");

            user.UserName = dto.NewUsername;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var msg = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to update username: {msg}");
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Invalid userId");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new InvalidOperationException("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var msg = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to delete user: {msg}");
            }
        }
    }
}
