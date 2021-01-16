using System;
using System.Collections.Generic;

namespace JWT_Auth.Microservice.Entities
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserUserRole = new HashSet<UserUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<UserUserRole> UserUserRole { get; set; }
    }
}
