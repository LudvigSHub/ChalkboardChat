using System;
using System.Linq;
using System.Threading.Tasks;
using ChalkboardChat.BLL.Common;
using ChalkboardChat.BLL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ChalkboardChat.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result> RegisterAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result.Fail("Username is required.");

            if (string.IsNullOrWhiteSpace(password))
                return Result.Fail("Password is required.");

            var existing = await _userManager.FindByNameAsync(username);
            if (existing != null)
                return Result.Fail("Username is already taken.");

            var user = new IdentityUser { UserName = username };

            var create = await _userManager.CreateAsync(user, password);
            if (!create.Succeeded)
                return Result.Fail(string.Join("; ", create.Errors.Select(e => e.Description)));

            //logga in direkt efter registrering
            await _signInManager.SignInAsync(user, isPersistent: false);

            return Result.Ok();
        }

        public async Task<Result> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return Result.Fail("Username and password are required.");

            var result = await _signInManager.PasswordSignInAsync(
                userName: username,
                password: password,
                isPersistent: false,
                lockoutOnFailure: false);

            return result.Succeeded
                ? Result.Ok()
                : Result.Fail("Invalid username or password.");
        }

        public async Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Result.Fail("UserId is required.");

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
                return Result.Fail("Old and new password are required.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Fail("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Succeeded
                ? Result.Ok()
                : Result.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        public async Task<Result> ChangeUsernameAsync(string userId, string newUsername)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Result.Fail("UserId is required.");

            if (string.IsNullOrWhiteSpace(newUsername))
                return Result.Fail("New username is required.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Fail("User not found.");

            var existing = await _userManager.FindByNameAsync(newUsername);
            if (existing != null && existing.Id != userId)
                return Result.Fail("Username is already taken.");

            user.UserName = newUsername;
            user.NormalizedUserName = _userManager.NormalizeName(newUsername);

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded
                ? Result.Ok()
                : Result.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        public async Task<Result> DeleteAccountAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Result.Fail("UserId is required.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Fail("User not found.");

            //utloggning innan delete
            await _signInManager.SignOutAsync();

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded
                ? Result.Ok()
                : Result.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}