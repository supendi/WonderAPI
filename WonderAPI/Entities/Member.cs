using System;

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
        public string Name { get; set; }
        public string Email { get; set; }
        public string OptionalEmail { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


    /// <summary>
    /// Represent MemberInfo model, it's actually a Member model, but without password
    /// </summary>
    public class MemberInfo
    {
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
        public string Email { get; set; }
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
