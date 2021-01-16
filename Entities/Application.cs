using System;
using System.Collections.Generic;

namespace JWT_Auth.Microservice.Entities
{
    public partial class Application
    {
        public Application()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
