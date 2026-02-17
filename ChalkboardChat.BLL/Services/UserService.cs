using ChalkboardChat.BLL.DTOs;
using ChalkboardChat.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ChangeUsernameAsync(UpdateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.NewUsername))
                throw new ArgumentException("Username cannot be empty");

            await _repository.UpdateUsernameAsync(dto.UserId, dto.NewUsername);
        }

        public async Task DeleteUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Invalid userId");

            await _repository.DeleteUserAsync(userId);
        }
    }
}
