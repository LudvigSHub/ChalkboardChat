using System;
using System.Collections.Generic;
using System.Text;
using ChalkboardChat.DAL.Data;
using ChalkboardChat.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MessageModel>> GetAllAsync()
        {
            return await _context.Messages
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task AddAsync(MessageModel message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
