using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpPost]
        public MemberInfo RegisterNewMember([FromBody]Member member)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                var existingMember = svc.RegisterNewMember(member);
                return new MemberInfo
                {
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
        [Route("{memberId}")]
        public MemberInfo GetMemberInfo([FromRoute]int memberId)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                var existingMember = svc.GetMember(memberId);
                return new MemberInfo
                {
                    Name = existingMember.Name,
                    Email = existingMember.Email,
                    OptionalEmail = existingMember.OptionalEmail,
                    MobileNumber = existingMember.MobileNumber,
                    Gender = existingMember.Gender,
                    DateOfBirth = existingMember.DateOfBirth
                };
            }
        }

        [HttpPost]
        [Route("auth")]
        public AuthInfo Authenticate([FromBody]LoginRequest loginRequest)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                return svc.Authenticate(loginRequest);
            }
        }
    }
}