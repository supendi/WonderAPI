using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WonderAPI.Controllers.Account.Inmem;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account.Tests
{
    [TestClass()]
    public class AuthServiceTests
    {
        private JWTHandler jwtHandler = new JWTHandler();
        private static IMemberRepository memberRepository;

        private static IMemberRepository getMemberRepo()
        {
            if (memberRepository == null)
            {
                var initialData = new List<Member>()
                {
                     new Member
                     {
                        ID = 1,
                        Name = "Yuni",
                        DateOfBirth = DateTime.Parse("1990-01-01"),
                        Email = "yuni@gmail.com",
                        Gender = "Female",
                        MobileNumber = "+123",
                        OptionalEmail = "",
                        Password = "myPasswordIsStrong"
                     }
                };
                memberRepository = new MemberRepository(initialData);
            }

            return memberRepository;
        }

        private AuthService GetAuthService()
        {
            var tokenList = new List<Token>()
            {
                 new Token
                 {
                    ID = 1,
                    AccessToken = Guid.NewGuid().ToString(),
                    RefreshToken = Guid.NewGuid().ToString(),
                    BlackListed=false,
                    CreatedAt = DateTime.Now,
                    ExpiredAt    = DateTime.Now.AddDays(2)
                 }
            };

            var service = new AuthService(new TokenRepository(tokenList), getMemberRepo(), jwtHandler, new BCryptHasher());
            return service;
        }

        private MemberService GetMemberService()
        {
            var service = new MemberService(getMemberRepo(), new BCryptHasher());
            return service;
        }

        [TestMethod()]
        public void AuthenticateTest()
        {
            var memberService = GetMemberService();
            memberService.RegisterNewMember(new Member
            {
                ID = 1,
                Name = "Arif",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Email = "arif@gmail.com",
                Gender = "Male",
                MobileNumber = "+123",
                OptionalEmail = "",
                Password = "myPasswordIsStrong"
            });
            var authService = GetAuthService();
            var authInfo = authService.Authenticate(new LoginRequest
            {
                Email = "arif@gmail.com",
                Password = "myPasswordIsStrong"
            });

            Assert.IsNotNull(authInfo);
            Assert.IsTrue(!string.IsNullOrEmpty(authInfo.AccessToken));
            Assert.IsTrue(!string.IsNullOrEmpty(authInfo.RefreshToken));
        }

        [TestMethod()]
        public void RenewAccessTokenTest()
        {
            var memberService = GetMemberService();
            memberService.RegisterNewMember(new Member
            {
                ID = 1,
                Name = "andri",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Email = "andri@gmail.com",
                Gender = "Male",
                MobileNumber = "+123",
                OptionalEmail = "",
                Password = "myPasswordIsStrong"
            });
            var authService = GetAuthService();
            var authInfo = authService.Authenticate(new LoginRequest
            {
                Email = "andri@gmail.com",
                Password = "myPasswordIsStrong"
            });
            var newAuthInfo = authService.RenewAccessToken(authInfo);
            Assert.IsNotNull(newAuthInfo);
            Assert.IsTrue(!string.IsNullOrEmpty(newAuthInfo.AccessToken));
            Assert.IsTrue(!string.IsNullOrEmpty(newAuthInfo.RefreshToken));
        }
    }
}