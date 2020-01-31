using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account
{
    public interface ITokenGenerator
    {
        string Generate(Member member);
    }

    public class TokenGenerator : ITokenGenerator
    {
        public string Generate(Member member)
        {
            throw new System.NotImplementedException();
        }
    }
}