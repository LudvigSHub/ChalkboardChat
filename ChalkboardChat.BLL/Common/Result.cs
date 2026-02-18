using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.BLL.Common
{
    public sealed class Result
    {
        public bool Success { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;

        public static Result Ok() => new Result { Success = true };
        public static Result Fail(string error) => new Result { Success = false, ErrorMessage = error };
    }
}
