using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Models.Enums
{
    public enum SecurityTypes
    {
        Aes128,
        Aes192,
        Aes256,
        Sha256,
        Sha384,
        Sha512,
        HmacSha256,
        HmacSha384,
        HmacSha512,
        RsaSha256,
        RsaSha384,
        RsaSha512
    }
}
