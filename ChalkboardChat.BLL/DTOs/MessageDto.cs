using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty; 
    }
}
