using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class MemberController : ControllerBase
    {
        [HttpPost]
        public MemberInfo RegisterNewMember([FromBody]Member member)
        {
            ModelValidator.Validate(member);
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                var newRegisteredMember = svc.RegisterNewMember(member);
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
        [HttpPut]
        public MemberInfo UpdateMember([FromBody]MemberUpdateRequest updateRequest)
        {
            ModelValidator.Validate(updateRequest);
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                var updatedMember = svc.UpdateMember(updateRequest);
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
        }

        [HttpPost]
        [Route("auth")]
        public AuthInfo Authenticate([FromBody]LoginRequest loginRequest)
        {
            ModelValidator.Validate(loginRequest);
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                return svc.Authenticate(loginRequest);
            }
        }
    }
}