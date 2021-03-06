﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Modules.Interafaces
{
    public interface IJwtTokenValidator
    {
        JWT.Auth.Entities.Token Token { get; }
        Task<bool> IsTokenValid();
    }
}
