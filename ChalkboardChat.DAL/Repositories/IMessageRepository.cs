using System;
using System.Collections.Generic;
using System.Text;
using ChalkboardChat.DAL.Models;

namespace ChalkboardChat.DAL.Repositories
{
    public interface IMessageRepository
    {
        Task<List<MessageModel>> GetAllAsync();
        Task AddAsync(MessageModel message);
        Task SaveChangesAsync();
    }
}
