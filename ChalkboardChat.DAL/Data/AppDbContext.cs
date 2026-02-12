using System;
using System.Collections.Generic;
using System.Text;
using ChalkboardChat.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MessageModel> Messages { get; set; }

        
    }
}
