using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account
{
    public interface ITokenGenerator
    {
        string Generate(Member member);
    }

    public class JWTGenerator : ITokenGenerator
    {
        static string secretKey = "";

        /// <summary>
        /// Returns secret key from env. If null return default key;
        /// </summary>
        /// <returns></returns>
        private static string GetSecretKey()
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                var jwtSecret = System.Environment.GetEnvironmentVariable("JwtSecret");
                secretKey = jwtSecret;
                if (secretKey == null)
                    secretKey = "WonderLabs";
            }

            return secretKey;
        }

        /// <summary>
        /// Generate JWT string
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Generate(Member member)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetSecretKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("name", member.Name),
                    new Claim("dateOfBirth", member.DateOfBirth.ToString()),
                    new Claim("gender", member.Gender),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}