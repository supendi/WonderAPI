using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace WonderAPI.Pkg
{
    /// <summary>
    /// A base class for internal controller.
    /// </summary>
    public class ApiControllerBase : ControllerBase
    {
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

        public string GetTokenPayloadValue(string claimType)
        {
            var token = GetJWTToken();
            if (token == null)
            {
                return null;
            }
            var claim = token.Claims.Where(x => x.Type == claimType).FirstOrDefault();
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }

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