using Microsoft.AspNetCore.Mvc;
using WonderAPI.Controllers.Account;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers
{
    /// <summary>
    /// Auth API entry point
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class AuthController : ApiControllerBase
    {
        private AuthService authService;

        public AuthController(ISecurityTokenHandler tokenHandler, AuthService authService) : base(tokenHandler)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Authorize user by providing username and password
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public AuthInfo Authenticate([FromBody]LoginRequest loginRequest)
        {
            ModelValidator.Validate(loginRequest);
            return authService.Authenticate(loginRequest);
        }

        /// <summary>
        /// Renew access token by providing its access token and refresh token
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh-token")]
        public AuthInfo RenewAccessToken([FromBody]AuthInfo request)
        {
            ModelValidator.Validate(request); 
            return authService.RenewAccessToken(request);
        }
    }
}