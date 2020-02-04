using Microsoft.VisualStudio.TestTools.UnitTesting;
using WonderAPI.Controllers.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using WonderAPI.Controllers.Account.Auth;

namespace WonderAPI.Controllers.Account.Tests
{
    [TestClass()]
    public class JWTGeneratorTests
    {
        [TestMethod()]
        public void GenerateTest()
        {
            var generator = new JWTHandler();
            var token = generator.GenerateAccessToken(new Entities.Member()
            {
                ID = 1,
                Name = "John Doe",
                Email = "john.doe@email.com",
                OptionalEmail = "",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                MobileNumber = "",
                Password = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot."
            });

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var x = jwtToken.Claims.Count();
            Assert.IsTrue(jwtToken.Claims.Count() == 6);
        }

        [TestMethod()]
        public void GetSecretKeyTest()
        {
            var key = JWTHandler.GetSecretKey();
            Assert.AreEqual(key, "akusayangkamuselamanyah");
        }
    }
}