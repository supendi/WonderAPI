using Microsoft.VisualStudio.TestTools.UnitTesting;
using WonderAPI.Controllers.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace WonderAPI.Controllers.Account.Tests
{
    [TestClass()]
    public class MemberServiceTests
    {
        MemberInmemRepository repo = new MemberInmemRepository(new List<Entities.Member>());
        IPasswordHasher hasher = new BCryptHasher();
        ITokenGenerator tokenGenerator = new JWTGenerator();

        [TestMethod()]
        public void MemberServiceTest()
        {
        }

        [TestMethod()]
        public void RegisterNewMemberTest()
        {
            var service = new MemberService(repo, hasher, tokenGenerator);
            var newMember = service.RegisterNewMember(new Entities.Member()
            {
                ID = 100,
                Name = "John Doe",
                Email = "john.doe@email.com",
                OptionalEmail = "",
                DateOfBirth = DateTime.Parse("1999-01-01"),
                Gender = "Male",
                MobileNumber = "",
                Password = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot."
            });

            var expectedNewMemberID = 1;
            var actualNewMemberID = newMember.ID;

            if (expectedNewMemberID != actualNewMemberID)
            {
                Assert.Fail("New member id should be {0}, but got {1}", expectedNewMemberID, actualNewMemberID);
            }

            var expectedRecordsCount = 1;
            var actualRecordsCount = repo.Members.Count;
            if (expectedRecordsCount != actualRecordsCount)
            {
                Assert.Fail("Records count should be {0}, but got {1}", expectedRecordsCount, actualRecordsCount);
            }

            var expectedRecord = new Entities.Member()
            {
                ID = 1,
                Name = "John Doe",
                Email = "john.doe@email.com",
                OptionalEmail = "",
                DateOfBirth = DateTime.Parse("1999-01-01"),
                Gender = "Male",
                MobileNumber = "",
                Password = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot."
            };

            var actualRecord = service.GetMember(newMember.ID);

            Assert.AreEqual(expectedRecord.ID, actualRecord.ID);
            Assert.AreEqual(expectedRecord.Name, actualRecord.Name);
            Assert.AreEqual(expectedRecord.Email, actualRecord.Email);
            Assert.AreEqual(expectedRecord.OptionalEmail, actualRecord.OptionalEmail);
            Assert.AreEqual(expectedRecord.DateOfBirth, actualRecord.DateOfBirth);
            Assert.AreEqual(expectedRecord.Gender, actualRecord.Gender);
            Assert.AreEqual(expectedRecord.MobileNumber, actualRecord.MobileNumber);
            Assert.IsTrue(new BCryptHasher().Verify(expectedRecord.Password, actualRecord.Password));
        }

        [TestMethod()]
        public void UpdateMemberTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AuthenticateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMemberTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DisposeTest()
        {
            Assert.Fail();
        }
    }
}