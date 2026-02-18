using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.DTOs
{
    public class UpdateUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string NewUsername { get; set; } = string.Empty;
    }
}
