using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Helpers
{
    public static class ExceptionHelper
    {        
        public static string GetInnerExceptions(Exception Exception, string Message = "", bool IsFile = false)
        {
            if (Exception == null) return Message;

            var breakLine = (IsFile) ? "\\r\\n" : string.Empty;

            var message = string.IsNullOrEmpty(Message) ? Exception.Message
                                                        : $"{Message}{breakLine} {Exception.Message}";

            return GetInnerExceptions(Exception.InnerException, message, IsFile);
        }
    }
}
