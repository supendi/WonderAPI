using Microsoft.AspNetCore.Mvc;
using WonderAPI.Controllers.Account;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers
{
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
        /// Authorize user by providing username and password
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh-token")]
        public AuthInfo RenewAccessToken([FromBody]AuthInfo authInfo)
        {
            ModelValidator.Validate(authInfo);
            return authService.RenewAccessToken(authInfo);
        }
    }
}