using Microsoft.AspNetCore.Mvc;
using WonderAPI.Pkg;
using WonderAPI.Pkg.Model;

namespace WonderAPI.Controllers.Account
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpGet]
        [Route("{memberId}")]
        public MemberInfo GetMemberInfo([FromRoute]int memberId)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new PasswordHasher(), new TokenGenerator()))
            {
                return svc.GetMemberInfo(memberId);
            }
        }

        [HttpPost]
        [Route("members")]
        public Member CreatMember([FromBody]Member member)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDBContext()), new PasswordHasher(), new TokenGenerator()))
            {
                return svc.RegisterNewMember(member);
            }
        }
    }
}