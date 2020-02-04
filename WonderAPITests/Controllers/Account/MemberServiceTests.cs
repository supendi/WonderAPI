using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WonderAPI.Controllers.Account.Inmem;

namespace WonderAPI.Controllers.Account.Tests
{
    [TestClass()]
    public class MemberServiceTests
    {
        MemberRepository repo = new MemberRepository(new List<Entities.Member>());
        IPasswordHasher hasher = new BCryptHasher();
        ISecurityTokenHandler tokenGenerator = new JWTHandler();

        [TestMethod()]
        public void MemberServiceTest()
        {
        }

        [TestMethod()]
        public void RegisterNewMemberTest()
        {
            var service = new MemberService(repo, hasher);
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
        public void RegisterNewMemberFailTest()
        {
            try
            {
                var service = new MemberService(repo, hasher);
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

                var newMember2 = service.RegisterNewMember(new Entities.Member()
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
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DuplicateEmailException);
            }
        }

        [TestMethod()]
        public void UpdateMemberTest()
        {
            var service = new MemberService(repo, hasher);
            var newMember = service.RegisterNewMember(new Entities.Member()
            {
                Name = "John Doe",
                Email = "john.doe@email.com",
                OptionalEmail = "",
                DateOfBirth = DateTime.Parse("1999-01-01"),
                Gender = "Male",
                MobileNumber = "",
                Password = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot."
            });

            var existingMember = service.GetMember(newMember.ID);
            existingMember.Name = "Changed name";
            existingMember.MobileNumber = "08123";
            var updatedMember = service.UpdateMember(new Entities.MemberUpdateRequest()
            {
                ID = existingMember.ID,
                Name = existingMember.Name,
                OptionalEmail = existingMember.OptionalEmail,
                DateOfBirth = existingMember.DateOfBirth,
                Gender = existingMember.Gender,
                MobileNumber = existingMember.MobileNumber,
            });

            var fecthedMember = service.GetMember(updatedMember.ID);
            Assert.AreEqual(fecthedMember.Name, updatedMember.Name);
            Assert.AreEqual(fecthedMember.MobileNumber, updatedMember.MobileNumber);
        }

        [TestMethod()]
        public void UpdateMemberFailTest()
        {
            try
            {
                var service = new MemberService(repo, hasher);
                var newMember = service.RegisterNewMember(new Entities.Member()
                {
                    Name = "John Doe",
                    Email = "john.doe@email.com",
                    OptionalEmail = "",
                    DateOfBirth = DateTime.Parse("1999-01-01"),
                    Gender = "Male",
                    MobileNumber = "",
                    Password = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot."
                });

                var existingMember = service.GetMember(newMember.ID);
                existingMember.Name = "Changed name";
                existingMember.MobileNumber = "08123";
                var updatedMember = service.UpdateMember(new Entities.MemberUpdateRequest()
                {
                    ID = 2,
                    Name = existingMember.Name,
                    OptionalEmail = existingMember.OptionalEmail,
                    DateOfBirth = existingMember.DateOfBirth,
                    Gender = existingMember.Gender,
                    MobileNumber = existingMember.MobileNumber,
                });
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is UserNotFoundException);
            }
        }

        //[TestMethod()]
        //public void AuthenticateTest()
        //{
        //    var service = new MemberService(repo, hasher);
        //    var plainPassword = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot.";
        //    var registrant = new Entities.Member()
        //    {
        //        Name = "John Doe",
        //        Email = "john.doe@email.com",
        //        OptionalEmail = "",
        //        DateOfBirth = DateTime.Parse("1999-01-01"),
        //        Gender = "Male",
        //        MobileNumber = "",
        //        Password = plainPassword
        //    };
        //    service.RegisterNewMember(registrant);
        //    var authInfo = service.Authenticate(new Entities.LoginRequest
        //    {
        //        Email = registrant.Email,
        //        Password = plainPassword
        //    });
        //    Assert.IsTrue(!string.IsNullOrEmpty(authInfo.Token));
        //}

        [TestMethod()]
        public void GetMemberTest()
        {
            var service = new MemberService(repo, hasher);
            var plainPassword = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot.";
            var registrant = new Entities.Member()
            {
                Name = "John Doe",
                Email = "john.doe@email.com",
                OptionalEmail = "",
                DateOfBirth = DateTime.Parse("1999-01-01"),
                Gender = "Male",
                MobileNumber = "",
                Password = plainPassword
            };
            service.RegisterNewMember(registrant);
            var existingMember = service.GetMember(registrant.ID);
            Assert.IsNotNull(existingMember);
            Assert.IsNotNull(existingMember.ID == 1);
        }

        [TestMethod()]
        public void DisposeTest()
        {
        }
    }
}