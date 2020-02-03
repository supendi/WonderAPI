using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using WonderAPI.Controllers.Account;

namespace WonderAPI.Pkg
{
    /// <summary>
    /// A base class for api controller.
    /// </summary>
    public class ApiControllerBase : ControllerBase
    {
        private ISecurityTokenHandler tokenHandler;

        public ApiControllerBase(ISecurityTokenHandler tokenHandler)
        {
            this.tokenHandler = tokenHandler;
        }
        /// <summary>
        /// Get token from current http request.
        /// </summary>
        /// <returns></returns>
        protected string GetAuthorizationHeaderValue()
        {
            var authHeaderValue = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeaderValue == null)
            {
                return null;
            }
            var accessToken = authHeaderValue.Replace("Bearer ", "");
            return accessToken;
        }

        /// <summary>
        /// Get member id from access token
        /// </summary>
        /// <returns></returns>
        public int GetMemberIDFromToken()
        {
            return tokenHandler.GetSubValue(GetAuthorizationHeaderValue());
        }
    }
}