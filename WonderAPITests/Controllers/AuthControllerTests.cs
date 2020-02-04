using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WonderAPI.Controllers.Account;
using WonderAPI.Controllers.Account.Inmem;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Tests
{
    [TestClass()]
    public class AuthControllerTests
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
            var memberController = new MemberController(jwtHandler, GetMemberService());
            var newMember = memberController.RegisterNewMember(new Member
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
            LoginRequest loginRequest = new LoginRequest
            {
                Email = newMember.Email,
                Password = "myPasswordIsStrong"
            };
            var authController = new AuthController(jwtHandler, GetAuthService());
            var authInfo = authController.Authenticate(loginRequest);

            Assert.IsNotNull(authInfo);
            Assert.IsTrue(!string.IsNullOrEmpty(authInfo.AccessToken));
        }

        [TestMethod()]
        public void AuthenticateFailInvalidEmailTest()
        {
            try
            {
                var memberController = new MemberController(jwtHandler, GetMemberService());
                var newAuth = memberController.RegisterNewMember(new Member
                {
                    ID = 1,
                    Name = "Wawan",
                    DateOfBirth = DateTime.Parse("1990-01-01"),
                    Email = "wawan@gmail.com",
                    Gender = "Male",
                    MobileNumber = "+123",
                    OptionalEmail = "",
                    Password = "myPasswordIsStrong"
                });
                LoginRequest loginRequest = new LoginRequest
                {
                    Email = "Invalid@email.com",
                    Password = "myPasswordIsStrong"
                };
                var authController = new AuthController(jwtHandler, GetAuthService());
                var authInfo = authController.Authenticate(loginRequest);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is AuthenticationException);
            }
        }

        [TestMethod()]
        public void AuthenticateFailValidationTest()
        {
            try
            {
                var memberController = new MemberController(jwtHandler, GetMemberService());
                var newMember = memberController.RegisterNewMember(new Member
                {
                    ID = 1,
                    Name = "Hani",
                    DateOfBirth = DateTime.Parse("1990-01-01"),
                    Email = "hani@gmail.com",
                    Gender = "Female",
                    MobileNumber = "+123",
                    OptionalEmail = "",
                    Password = "myPasswordIsStrong"
                });
                LoginRequest loginRequest = new LoginRequest
                {
                    Email = "",
                    Password = "myPasswordIsStrong"
                };
                var authController = new AuthController(jwtHandler, GetAuthService());
                var authInfo = authController.Authenticate(loginRequest);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is WonderAPI.Pkg.ValidationException);
                var vex = ex as WonderAPI.Pkg.ValidationException;
                //error count is only 1
                Assert.IsTrue(vex.ValidationResult.Errors.Count == 1);

                //and the error is caused by empty name
                Assert.IsTrue(vex.ValidationResult.Errors.Where(x => x.Field == nameof(Member.Email)).Count() == 1);
            }

        }

        [TestMethod()]
        public void RenewAccessTokenTest()
        {
            var memberController = new MemberController(jwtHandler, GetMemberService());
            var newMember = memberController.RegisterNewMember(new Member
            {
                ID = 1,
                Name = "Anggie",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Email = "anggie@gmail.com",
                Gender = "Female",
                MobileNumber = "+123",
                OptionalEmail = "",
                Password = "myPasswordIsStrong"
            });
            LoginRequest loginRequest = new LoginRequest
            {
                Email = "anggie@gmail.com",
                Password = "myPasswordIsStrong"
            };
            var authController = new AuthController(jwtHandler, GetAuthService());
            var authInfo = authController.Authenticate(loginRequest);
            var newAuthInfo = authController.RenewAccessToken(authInfo);
            Assert.IsNotNull(newAuthInfo);
            Assert.IsTrue(!string.IsNullOrEmpty(newAuthInfo.AccessToken));
            Assert.IsTrue(!string.IsNullOrEmpty(newAuthInfo.RefreshToken));
        }
    }
}