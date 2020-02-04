using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace WonderAPI.Controllers.Account.Inmem.Tests
{
    [TestClass()]
    public class TokenRepositoryTests
    {
        private List<Entities.Token> InitializeData()
        {
            var data = new List<Entities.Token>();
            data.Add(new Entities.Token()
            {
                ID = 1,
                AccessToken = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString(),
                BlackListed = false,
                CreatedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(2),
            });

            return data;
        }

        [TestMethod()]
        public void AddTest()
        {
            var repo = new TokenRepository(InitializeData());
            var newAccesstoken = Guid.NewGuid().ToString();
            var newRefreshToken = Guid.NewGuid().ToString();
            repo.Add(new Entities.Token()
            {
                ID = 1,
                AccessToken = newAccesstoken,
                RefreshToken = newRefreshToken,
                BlackListed = false,
                CreatedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(2),
            });
            Assert.IsTrue(repo.Tokens.Count == 2);
            Assert.IsTrue(repo.Tokens[1].ID == 2);
            Assert.IsTrue(repo.Tokens[1].AccessToken == newAccesstoken);
            Assert.IsTrue(repo.Tokens[1].RefreshToken == newRefreshToken);
        }

        [TestMethod()]
        public void GetByRefreshTokenTest()
        {
            var repo = new TokenRepository(InitializeData());
            var newAccesstoken = Guid.NewGuid().ToString();
            var newRefreshToken = Guid.NewGuid().ToString();
            repo.Add(new Entities.Token()
            {
                ID = 1,
                AccessToken = newAccesstoken,
                RefreshToken = newRefreshToken,
                BlackListed = false,
                CreatedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(2),
            });
            var token = repo.GetByRefreshToken(newRefreshToken);
            Assert.IsNotNull(token);
            Assert.IsTrue(token.RefreshToken == newRefreshToken);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var repo = new TokenRepository(InitializeData());
            var someToken = repo.GetById(1);
            Assert.AreEqual(1, someToken.ID);
        }

        [TestMethod()]
        public void BlackListTest()
        {
            var repo = new TokenRepository(InitializeData());
            var someToken = repo.BlackList(1);
            var blacklistedToken = repo.GetById(1);
            Assert.IsTrue(blacklistedToken.BlackListed);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var repo = new TokenRepository(InitializeData());
            repo.Delete(1);
            var deletedToken = repo.GetById(1);
            Assert.IsNull(deletedToken);
        }

        [TestMethod()]
        public void DisposeTest()
        {
            var repo = new TokenRepository(InitializeData());
            repo.Dispose();
            Assert.IsNull(repo.Tokens); 
        }
    }
}