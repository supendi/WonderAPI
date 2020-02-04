using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Account.Auth
{
    /// <summary>
    /// This exeption will be thrown if user failed to login
    /// </summary>
    public class AuthenticationException : AppException
    {
        public AuthenticationException() : base("Invalid email or password.")
        {
        }
    }

    /// <summary>
    /// Thrown if refresh token provided by user is invalid
    /// </summary>
    public class InvalidTokenException : AppException
    {
        public InvalidTokenException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Specify functionality for working with token storage
    /// </summary>
    public interface ITokenRepository : IDisposable
    {
        Token Add(Token token);
        void Delete(int tokenID);
        Token BlackList(int tokenID);
        Token GetByRefreshToken(string refreshToken);
    }

    /// <summary>
    /// Specify
    /// </summary>
    public class AuthService : IDisposable
    {
        ISecurityTokenHandler tokenHandler;
        IMemberRepository memberRepository;
        IPasswordHasher passwordHasher;
        ITokenRepository tokenRepository;

        public AuthService(ITokenRepository tokenRepository, IMemberRepository memberRepository, ISecurityTokenHandler tokenHandler, IPasswordHasher passwordHasher)
        {
            this.tokenRepository = tokenRepository;
            this.memberRepository = memberRepository;
            this.tokenHandler = tokenHandler;
            this.passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Authenticates user by providing its email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthInfo Authenticate(LoginRequest loginRequest)
        {
            var member = memberRepository.GetByEmail(loginRequest.Email);
            if (member == null)
                throw new AuthenticationException();

            if (!passwordHasher.Verify(loginRequest.Password, member.Password))
                throw new AuthenticationException();

            var now = DateTime.Now;
            var newAccessToken = tokenHandler.GenerateAccessToken(member);
            var newRefreshToken = tokenHandler.GenerateRefreshToken();
            
            tokenRepository.Add(new Token()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                BlackListed = false,
                ExpiredAt = now.AddDays(5),
                CreatedAt = now,
            });

            return new AuthInfo
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        /// <summary>
        /// Renew an access token by exchanging it with the given refresh token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthInfo RenewAccessToken(AuthInfo request)
        {
            var tokenRecord = tokenRepository.GetByRefreshToken(request.RefreshToken);
            if (tokenRecord == null)
            {
                throw new InvalidTokenException("Invalid refresh token.");
            }

            //Access token should be verified.
            //because on the token storage it's been save as a paired token (access and refresh token are saved together)
            if (tokenRecord.AccessToken != request.AccessToken)
            {
                throw new InvalidTokenException("Invalid access token.");
            }

            var memberID = tokenHandler.GetSubValue(tokenRecord.AccessToken);
            var member = memberRepository.GetById(memberID);
            if (member == null)
                throw new UserNotFoundException();

            var newAccessToken = tokenHandler.GenerateAccessToken(member);
            var newRefreshToken = tokenHandler.GenerateRefreshToken();

            var now = DateTime.Now;

            tokenRepository.BlackList(tokenRecord.ID);

            tokenRepository.Add(new Token()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                BlackListed = false,
                ExpiredAt = now.AddDays(5),
                CreatedAt = now,
            });

            return new AuthInfo
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        } 

        public void Dispose()
        {
            tokenRepository.Dispose();
            memberRepository.Dispose();
        }
    }
}
