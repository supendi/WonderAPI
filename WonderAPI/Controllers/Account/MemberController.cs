using Microsoft.AspNetCore.Mvc;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpGet]
        [Route("{memberId}")]
        public MemberInfo GetMemberInfo([FromRoute]int memberId)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                return svc.GetMemberInfo(memberId);
            }
        }

        [HttpPost]
        public Member CreateMember([FromBody]Member member)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new Pbkdf2Hasher(), new JWTGenerator()))
            {
                return svc.RegisterNewMember(member);
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