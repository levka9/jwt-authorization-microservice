using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Helpers
{
    public static class ExceptionHelper
    {        
        public static string GetInnerExceptions(this Exception ex, string msgs = "")
        {
            if (ex == null) return string.Empty;
            if (msgs == "") msgs = ex.Message;
            if (ex.InnerException != null)
                msgs += "\r\nInnerException: " + ExceptionHelper.GetInnerExceptions(ex.InnerException);
            return msgs;
        }
    }
}
