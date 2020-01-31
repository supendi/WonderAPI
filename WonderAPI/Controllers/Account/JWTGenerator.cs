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
        public static string GetSecretKey()
        {
            if (string.IsNullOrEmpty(secretKey))
            {
                var jwtSecret = System.Environment.GetEnvironmentVariable("JwtSecret");
                secretKey = jwtSecret;
                if (secretKey == null)
                    secretKey = "akusayangkamuselamanyah";
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
            var jwtKey = Encoding.ASCII.GetBytes(GetSecretKey());
            var securityKey = new SymmetricSecurityKey(jwtKey);

            var newToken = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.GivenName, member.Name),
                    new Claim(JwtRegisteredClaimNames.Birthdate, member.DateOfBirth.ToString()),
                    new Claim(JwtRegisteredClaimNames.Gender, member.Gender),
                },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(newToken);
            return tokenString;
        }
    }
}