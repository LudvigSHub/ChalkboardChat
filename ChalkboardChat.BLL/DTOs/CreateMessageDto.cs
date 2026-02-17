using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.DTOs
{
    public class CreateMessageDto
    {
        public string Message { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
