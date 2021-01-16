﻿using System;
using System.Collections.Generic;

namespace JWT_Auth.Microservice.Entities
{
    public partial class User
    {
        public User()
        {
            Token = new HashSet<Token>();
            UserField = new HashSet<UserField>();
            UserUserRole = new HashSet<UserUserRole>();
        }

        public long Id { get; set; }
        public int ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PassportIdentity { get; set; }
        public string PicturePath { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthdate { get; set; }
        public float UtcOffset { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLocked { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
        public byte FailedPasswordAttemptCount { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Application Application { get; set; }
        public virtual ICollection<Token> Token { get; set; }
        public virtual ICollection<UserField> UserField { get; set; }
        public virtual ICollection<UserUserRole> UserUserRole { get; set; }
    }
}
