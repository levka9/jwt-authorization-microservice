﻿using JWT_Auth.Microservice.Entities;
using JWT.Auth.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Models.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public ApplicationType ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PicturePath { get; set; }
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Comments { get; set; }

        public ICollection<UserField> UserFields { get; set; }
        public ICollection<UserUserRole> UserRoles { get; set; }
    }
}
