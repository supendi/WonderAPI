using Microsoft.AspNetCore.Mvc;
using WonderAPI.Entities;
using WonderAPI.Pkg;

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
            using (var svc = new MemberService(new MemberRepository(new WonderDbContext()), new Pbkdf2Hasher(), new TokenGenerator()))
            {
                return svc.GetMemberInfo(memberId);
            }
        }

        [HttpPost] 
        public Member CreatMember([FromBody]Member member)
        {
            using (var svc = new MemberService(new MemberRepository(new WonderDbContext()), new Pbkdf2Hasher(), new TokenGenerator()))
            {
                return svc.RegisterNewMember(member);
            }
        }
    }
}