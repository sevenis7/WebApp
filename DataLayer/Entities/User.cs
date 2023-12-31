﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public enum Role
    {
        User = 0,
        Admin = 1
    }

    public class User
    {
        public Guid UserId { get; set; }

        public string Login { get; set; } = null!;

        public string FirstName { get; set; }= null!;

        public string LastName { get; set; } = null!;

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        public Role Role { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        //references
        //public ICollection<Request>? Requests { get; set; }

        public ICollection<RefreshToken>? RefreshTokens { get; set; }

    }
}
