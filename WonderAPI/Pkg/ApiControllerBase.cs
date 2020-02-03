using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace WonderAPI.Pkg
{
    /// <summary>
    /// A base class for api controller.
    /// </summary>
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Get token from current http request.
        /// </summary>
        /// <returns></returns>
        public JwtSecurityToken GetJWTToken()
        {
            var authHeaderValue = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeaderValue == null)
            {
                return null;
            }
            var accessToken = authHeaderValue.Replace("Bearer ", "");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            return jwtToken;
        }

        /// <summary>
        /// Get token payload value by specified claim type/payload key
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public string GetTokenPayloadValue(string claimType)
        {
            var token = GetJWTToken();
            if (token == null)
            {
                return null;
            }
            var claim = token.Claims.FirstOrDefault(x => x.Type == claimType);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }

        /// <summary>
        /// Get member id from access token
        /// </summary>
        /// <returns></returns>
        public int GetMemberIDFromToken()
        {
            var memberIDFromToken = GetTokenPayloadValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(memberIDFromToken))
            {
                throw new AppException("Cannot read member ID from token.");
            }
            var parseOK = int.TryParse(memberIDFromToken, out int memberID);
            if (!parseOK)
            {
                throw new AppException("Cannot parse member ID from token.");
            }
            return memberID;
        }
    }
}