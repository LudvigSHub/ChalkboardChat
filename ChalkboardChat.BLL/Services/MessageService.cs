using ChalkboardChat.BLL.DTOs;
using ChalkboardChat.BLL.Interfaces;
using ChalkboardChat.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;

        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MessageDto>> GetAllMessagesAsync()
        {
            var messages = await _repository.GetAllAsync();

            return messages
                .OrderByDescending(m => m.Date)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Date = m.Date,
                    Message = m.Message,
                    Username = m.Username
                })
                .ToList();
        }

        public async Task CreateMessageAsync(CreateMessageDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Message))
                throw new ArgumentException("Message cannot be empty");

            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new ArgumentException("Username is required");

            var message = new Message
            {
                Date = DateTime.UtcNow,
                Message = dto.Message,
                Username = dto.Username
            };

            await _repository.AddAsync(message);
        }
    }
}
