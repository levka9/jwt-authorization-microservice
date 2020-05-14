using System;
using System.Collections.Generic;

namespace JWT.Auth.Entities
{
    public partial class UserUserRole
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int UserRoleId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
