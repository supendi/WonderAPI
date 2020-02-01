using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WonderAPI.Controllers.Account.Tests
{
    [TestClass()]
    public class BCryptHasherTests
    {
        [TestMethod()]
        public void HashTest()
        {
            var hasher = new BCryptHasher();

            var password = "aW3akp455w0rD";
            var hashedPassword = hasher.Hash(password);

            Assert.IsTrue(hasher.Verify(password, hashedPassword));
        }
    }
}