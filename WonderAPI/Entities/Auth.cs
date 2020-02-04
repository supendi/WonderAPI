using System;
using System.ComponentModel.DataAnnotations;

namespace WonderAPI.Entities
{
    /// <summary>
    /// Represent token model
    /// </summary>
    public class Token
    {
        public int ID { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        public bool BlackListed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }

    /// <summary>
    /// Represent the login model
    /// </summary>
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Represent the response model if a user successfully authenticated
    /// </summary>
    public class AuthInfo
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
