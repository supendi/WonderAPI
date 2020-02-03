using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// Access token interface
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generate access token
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        string Generate(Member member);
    }

    /// <summary>
    /// JWTGenerator implements ITokenGenerator, generates JWT
    /// </summary>
    public class JWTGenerator : ITokenGenerator
    {
        static string secretKey = "";
        static SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GetSecretKey()));

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
                if (string.IsNullOrEmpty(secretKey))
                    secretKey = "akusayangkamuselamanyah";
            }

            return secretKey;
        }

        /// <summary>
        /// Register authorization to API
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterAuth(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        /// <summary>
        /// Generate JWT string
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public string Generate(Member member)
        {
            var newToken = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, member.ID.ToString()),
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