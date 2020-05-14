using System;
using System.Collections.Generic;

namespace JWT.Auth.Entities
{
    public partial class Applications
    {
        public Applications()
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
