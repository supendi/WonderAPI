using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// Member API entry point
    /// </summary>
    [Route("api/members")]
    [ApiController]
    [ApiExceptionFilter]
    public class MemberController : ApiControllerBase
    {
        /// <summary>
        /// When using this injected object, it is no need to use the 'using' keyword. 
        /// Because, any dependencies that created by container, will be disposed by container too
        /// see: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1#overview-of-dependency-injection.
        /// </summary>
        MemberService memberService;

        public MemberController(ISecurityTokenHandler tokenHandler, MemberService memberService) : base(tokenHandler)
        {
            this.memberService = memberService;
        }

        /// <summary>
        /// Register a new member
        /// </summary>
        /// <param name="registrant"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public MemberInfo RegisterNewMember([FromBody]Member registrant)
        {
            ModelValidator.Validate(registrant);
            var newRegisteredMember = memberService.RegisterNewMember(registrant);
            return new MemberInfo
            {
                ID = newRegisteredMember.ID,
                Name = newRegisteredMember.Name,
                Email = newRegisteredMember.Email,
                OptionalEmail = newRegisteredMember.OptionalEmail,
                MobileNumber = newRegisteredMember.MobileNumber,
                Gender = newRegisteredMember.Gender,
                DateOfBirth = newRegisteredMember.DateOfBirth
            };
        }

        /// <summary>
        /// Get member by ID. Member ID value is taken from URL
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{memberId}")]
        public MemberInfo GetMemberInfo([FromRoute]int memberId)
        {
            using (memberService)
            {
                var existingMember = memberService.GetMember(memberId);
                return new MemberInfo
                {
                    ID = existingMember.ID,
                    Name = existingMember.Name,
                    Email = existingMember.Email,
                    OptionalEmail = existingMember.OptionalEmail,
                    MobileNumber = existingMember.MobileNumber,
                    Gender = existingMember.Gender,
                    DateOfBirth = existingMember.DateOfBirth
                };
            }
        }

        [Authorize]
        [HttpGet]
        [Route("me")]
        public MemberInfo GetCurrentMemberInfo()
        {
            var memberId = GetMemberIDFromToken();
            using (memberService)
            {
                var existingMember = memberService.GetMember(memberId);
                return new MemberInfo
                {
                    ID = existingMember.ID,
                    Name = existingMember.Name,
                    Email = existingMember.Email,
                    OptionalEmail = existingMember.OptionalEmail,
                    MobileNumber = existingMember.MobileNumber,
                    Gender = existingMember.Gender,
                    DateOfBirth = existingMember.DateOfBirth
                };
            }
        }

        /// <summary>
        /// Updates an existing member
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public MemberInfo UpdateMember([FromBody]MemberUpdateRequest updateRequest)
        {
            ModelValidator.Validate(updateRequest);
            var updatedMember = memberService.UpdateMember(updateRequest);
            return new MemberInfo
            {
                ID = updatedMember.ID,
                Name = updatedMember.Name,
                Email = updatedMember.Email,
                OptionalEmail = updatedMember.OptionalEmail,
                MobileNumber = updatedMember.MobileNumber,
                Gender = updatedMember.Gender,
                DateOfBirth = updatedMember.DateOfBirth
            };
        }

        ///// <summary>
        ///// Authorize user by providing username and password
        ///// </summary>
        ///// <param name="loginRequest"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("auth")]
        //public AuthInfo Authenticate([FromBody]LoginRequest loginRequest)
        //{
        //    ModelValidator.Validate(loginRequest);
        //    return memberService.Authenticate(loginRequest);
        //}
    }
}