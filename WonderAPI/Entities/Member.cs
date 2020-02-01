using System;
using System.ComponentModel.DataAnnotations;

namespace WonderAPI.Entities
{
    /// <summary>
    /// Gender enumeration
    /// </summary>
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    /// <summary>
    /// Represent Member business entity
    /// </summary>
    public class Member
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string OptionalEmail { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Password { get; set; }

        [Required]
        [MaxLength(24)]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }

    /// <summary>
    /// Represent update member request model
    /// </summary>
    public class MemberUpdateRequest
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } 

        public string OptionalEmail { get; set; }

        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }


    /// <summary>
    /// Represent MemberInfo model, it's actually a Member model, but without password
    /// </summary>
    public class MemberInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OptionalEmail { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


    /// <summary>
    /// Represent the login model
    /// </summary>
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represent the response model if a user successfully authenticated
    /// </summary>
    public class AuthInfo
    {
        public string Token { get; set; }
    }
}
