﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace WonderAPI.Controllers.Account.Tests
{
    [TestClass()]
    public class JWTHandlerTests
    {
        [TestMethod()]
        public void GenerateTest()
        {
            var tokenHandler = new JWTHandler();
            var token = tokenHandler.GenerateAccessToken(new Entities.Member()
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