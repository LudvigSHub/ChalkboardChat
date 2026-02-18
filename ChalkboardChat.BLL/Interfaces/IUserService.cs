using ChalkboardChat.BLL.Common;
using ChalkboardChat.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterAsync(string username, string password);
        Task<Result> LoginAsync(string username, string password);

        // Lägg till dessa när ni tar nästa sidor:
        Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<Result> ChangeUsernameAsync(string userId, string newUsername);
        Task<Result> DeleteAccountAsync(string userId);
    }
}
