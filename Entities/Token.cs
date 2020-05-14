using System;
using System.Collections.Generic;

namespace JWT.Auth.Entities
{
    public partial class Token
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public byte SecurityTypeId { get; set; }
        public string TokenKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SystemName { get; set; }
        public string Ip { get; set; }
        public string BrowserCapabilities { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual SecurityType SecurityType { get; set; }
        public virtual User User { get; set; }
    }
}
