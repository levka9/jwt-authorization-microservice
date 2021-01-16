using System;
using System.Collections.Generic;

namespace JWT_Auth.Microservice.Entities
{
    public partial class UserEmail
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Email { get; set; }
    }
}
