using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core.Models;
using JWT.Auth.Models;
using JWT_Auth.Microservice.Modules.Interafaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Auth.Microservice.Modules
{
    public class UserEmailModule : IUserEmailModule
    {
        #region Properties
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ISender sendGridSender;
        IConfiguration configuration; 
        #endregion

        public UserEmailModule(ISender SendGridSender, IConfiguration Configuration)
        {
            sendGridSender = SendGridSender;
            configuration = Configuration;
        }

        public async Task<SendResponse> SendChangedPasswordNotification(string EmailTo, string Fullname, float? UtcOffset)
        {
            string template = $"Dear {Fullname}, <br />your password to service FindLang.com changed at {DateTime.UtcNow.AddHours((double)UtcOffset)}.";

            var email = Email.From(configuration.GetSection("SendGrid:SenderDefaultEmail").Value)
                                          .To(EmailTo)
                                          .Subject("your password changed")
                                          .Body(template, true);

            var sendResponse = await sendGridSender.SendAsync(email);

            CheckResponse(sendResponse);

            return sendResponse;
        }

        public async Task<SendResponse> SendPassword(string EmailTo, string Fullname, string Password, string SystemName)
        {
            string template = $"Dear {Fullname},<br /> Your password: {Password}";

            var email = Email.From(configuration.GetSection("SendGrid:SenderDefaultEmail").Value)
                             .To(EmailTo)
                             .Subject($"password to {SystemName}")
                             .Body(template, true);

            var sendResponse = await sendGridSender.SendAsync(email);

            CheckResponse(sendResponse);

            return sendResponse;
        }

        private void CheckResponse(SendResponse sendResponse)
        {
            if(!sendResponse.Successful)
            {
                log.Error(sendResponse.ErrorMessages);
            }
        }
    }
}
