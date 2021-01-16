using System;
using System.Collections.Generic;

namespace JWT_Auth.Microservice.Entities
{
    public partial class SecurityType
    {
        public SecurityType()
        {
            Token = new HashSet<Token>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Token> Token { get; set; }
    }
}
