using System;
using System.Collections.Generic;

namespace JWT.Auth.Entities
{
    public partial class UserField
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FieldType { get; set; }
        public string FieldName { get; set; }
        public string FieldData { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
    }
}
