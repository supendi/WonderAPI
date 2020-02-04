using Microsoft.VisualStudio.TestTools.UnitTesting;
using WonderAPI.Controllers.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WonderAPI.Controllers.Account.Inmem;

namespace WonderAPI.Controllers.Account.Tests
{
    [TestClass()]
    public class MemberInmemRepositoryTests
    {

        private List<Entities.Member> InitializeData()
        {
            var data = new List<Entities.Member>();
            data.Add(new Entities.Member()
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

            return data;

        }
        [TestMethod()]
        public void MemberInmemRepositoryTest()
        {
            var repo = new MemberRepository(InitializeData());
            Assert.IsTrue(repo.Members.Count() == 1);
        }

        [TestMethod()]
        public void AddTest()
        {
            var repo = new MemberRepository(InitializeData());
            repo.Add(new Entities.Member()
            {
                Name = "Anna",
                Email = "anna@email.com",
                OptionalEmail = "",
                DateOfBirth = DateTime.Now,
                Gender = "Female",
                MobileNumber = "+12345",
                Password = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot."
            });
            Assert.IsTrue(repo.Members.Count == 2);
            Assert.IsTrue(repo.Members[1].ID == 2);
            Assert.IsTrue(repo.Members[1].Name == "Anna");
        }

        [TestMethod()]
        public void GetByEmailTest()
        {
            var repo = new MemberRepository(InitializeData());
            var someMember = repo.GetByEmail("john.doe@email.com");
            Assert.AreEqual("john.doe@email.com", someMember.Email);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var repo = new MemberRepository(InitializeData());
            var someMember = repo.GetById(1);
            Assert.AreEqual(1, someMember.ID);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var repo = new MemberRepository(InitializeData());
            var existingMember = new Entities.Member()
            {
                ID = 1,
                Name = "John Doe",
                Email = "goodguy@email.com",
                OptionalEmail = "secondary@email.com",
                DateOfBirth = DateTime.Now,
                Gender = "Male",
                MobileNumber = "",
                Password = "ASuperDuperStrongPasswordThatNoOneCanHack.YesItUseADot."
            };
            var updatedMember = repo.Update(existingMember);
            Assert.IsNotNull(updatedMember);
            var fetchedMember = repo.GetById(updatedMember.ID);
            Assert.IsTrue(fetchedMember.OptionalEmail == "secondary@email.com");
            Assert.IsTrue(fetchedMember.Email == "john.doe@email.com"); //Make sure that primary email is not changed
        }

        [TestMethod()]
        public void DisposeTest()
        {
            var repo = new MemberRepository(InitializeData());
            repo.Dispose();
            Assert.IsNull(repo.Members);
        }
    }
}