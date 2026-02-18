using ChalkboardChat.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetAllMessagesAsync();
        Task CreateMessageAsync(CreateMessageDto dto);
    }
}
