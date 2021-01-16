﻿using FluentEmail.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Auth.Microservice.Modules.Interafaces
{
    public interface IUserEmailModule
    {
        Task<SendResponse> SendChangedPasswordNotification(string EmailTo, string Fullname, float? UtcOffset);
    }
}