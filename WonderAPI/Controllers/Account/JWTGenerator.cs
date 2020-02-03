using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// Specify functionality to handle Token
    /// </summary>
    public interface ISecurityTokenHandler
    {
        /// <summary>
        /// Generate access token
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        string GenerateAccessToken(Member member);

        /// <summary>
        /// Generate a refresh token
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Get payload/claim value from an encoded access token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="keyOrClaimType"></param>
        /// <returns></returns>
        string GetValue(string token, string keyOrClaimType);
    }

    /// <summary>
    /// JWTGenerator implements ITokenGenerator, generates JWT
    /// </summary>
    public class JWTHandler : ISecurityTokenHandler
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
        public string GenerateAccessToken(Member member)
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

        /// <summary>
        /// Generates refresh token
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Get payload/claim value from an encoded access token.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="keyOrClaimType"></param>
        /// <returns></returns>
        public string GetValue(string accessToken, string keyOrClaimType)
        {
            var jwt = Decode(accessToken);
            if (jwt == null)
            {
                return null;
            }
            var claim = jwt.Claims.FirstOrDefault(x => x.Type == keyOrClaimType);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }

        /// <summary>
        /// Decode an encoded access token to JWT token.
        /// </summary>
        /// <returns></returns>
        protected JwtSecurityToken Decode(string accessToken)
        { 
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            return jwtToken;
        }
    }
}