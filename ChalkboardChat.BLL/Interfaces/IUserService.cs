using ChalkboardChat.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Interfaces
{
    public interface IUserService
    {
        Task ChangeUsernameAsync(UpdateUserDto dto);
        Task DeleteUserAsync(string userId);
    }
}
