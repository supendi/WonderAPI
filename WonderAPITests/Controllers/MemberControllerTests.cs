using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WonderAPI.Controllers.Account;
using WonderAPI.Controllers.Account.Inmem;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Tests
{
    [TestClass()]
    public class MemberControllerTests
    {
        private JWTHandler jwtHandler = new JWTHandler();
        private MemberService GetMemberService()
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

            var service = new MemberService(new MemberRepository(initialData), new BCryptHasher());
            return service;
        }

        [TestMethod()]
        public void RegisterNewMemberTest()
        {
            var ctrl = new MemberController(jwtHandler, GetMemberService());
            var newMember = ctrl.RegisterNewMember(new Member
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
            Assert.IsTrue(newMember.ID == 2);
            var member = ctrl.GetMemberInfo(2);
            Assert.IsNotNull(member);
            Assert.IsTrue(member.Email == "wawan@gmail.com");
        }

        [TestMethod()]
        public void RegisterNewMemberFailValidationTest()
        {
            try
            {
                var ctrl = new MemberController(jwtHandler, GetMemberService());
                var newMember = ctrl.RegisterNewMember(new Member
                {
                    ID = 1,
                    Name = "",
                    DateOfBirth = DateTime.Parse("1990-01-01"),
                    Email = "wawan@gmail.com",
                    Gender = "Male",
                    MobileNumber = "+123",
                    OptionalEmail = "",
                    Password = "myPasswordIsStrong"
                });
                throw new Exception("Dont hit this exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ValidationException);
                var vex = ex as ValidationException;
                //error count is only 1
                Assert.IsTrue(vex.ValidationResult.Errors.Count == 1);

                //and the error is caused by empty name
                Assert.IsTrue(vex.ValidationResult.Errors.Where(x => x.Field == nameof(Member.Name)).Count() == 1);
            }
        }

        [TestMethod()]
        public void RegisterNewMemberFailDuplicateEmailTest()
        {
            try
            {
                var ctrl = new MemberController(jwtHandler, GetMemberService());
                var newMember = ctrl.RegisterNewMember(new Member
                {
                    Name = "Yuni",
                    DateOfBirth = DateTime.Parse("1990-01-01"),
                    Email = "yuni@gmail.com",
                    Gender = "Male",
                    MobileNumber = "+123",
                    OptionalEmail = "",
                    Password = "myPasswordIsStrong"
                });
                throw new Exception("Dont hit this exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is DuplicateEmailException);
            }
        }

        [TestMethod()]
        public void GetMemberInfoTest()
        {
            var ctrl = new MemberController(jwtHandler, GetMemberService());
            var memberInfo = ctrl.GetMemberInfo(1);
            Assert.IsNotNull(memberInfo);
            Assert.IsTrue(memberInfo.Email == "yuni@gmail.com");
        }

        public void GetMemberInfoFailTest()
        {
            try
            {
                var ctrl = new MemberController(jwtHandler, GetMemberService());
                var memberInfo = ctrl.GetMemberInfo(2);
                throw new Exception("You fail if you hit me.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is UserNotFoundException);
            }
        }

        [TestMethod()]
        public void UpdateMemberTest()
        {
            var ctrl = new MemberController(jwtHandler, GetMemberService());

            var updatedMember = ctrl.UpdateMember(new MemberUpdateRequest
            {
                ID = 1,
                Name = "Yuni Shara",
                DateOfBirth = DateTime.Parse("1990-01-01"),
                Gender = "Female",
                MobileNumber = "+123",
                OptionalEmail = "",
            });
            var existingMember = ctrl.GetMemberInfo(1);
            Assert.AreEqual(existingMember.Name, "Yuni Shara");
            Assert.AreEqual(existingMember.Gender, "Female");
        }

        [TestMethod()]
        public void UpdateMemberFailUserNotFoundTest()
        {
            try
            {
                var ctrl = new MemberController(jwtHandler, GetMemberService());

                var updatedMember = ctrl.UpdateMember(new MemberUpdateRequest
                {
                    ID = 1000,
                    Name = "Yuni Shara",
                    DateOfBirth = DateTime.Parse("1990-01-01"),
                    Gender = "Female",
                    MobileNumber = "+123",
                    OptionalEmail = "",
                });
                throw new Exception("You fail");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is UserNotFoundException);
            }
        }

        [TestMethod()]
        public void UpdateMemberFailValidationTest()
        {
            try
            {
                var ctrl = new MemberController(jwtHandler, GetMemberService());

                var updatedMember = ctrl.UpdateMember(new MemberUpdateRequest
                {
                    ID = 1,
                    Name = "Yuni Shara",
                    DateOfBirth = DateTime.Parse("1990-01-01"),
                    Gender = "",
                    MobileNumber = "",
                    OptionalEmail = "",
                });
                throw new Exception("You fail");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ValidationException);
                var vex = ex as ValidationException;
                //error count is only 1
                Assert.IsTrue(vex.ValidationResult.Errors.Count == 2);

                //and the error is caused by empty name
                Assert.IsTrue(vex.ValidationResult.Errors.Where(x => x.Field == nameof(Member.Gender)).Count() == 1);
                Assert.IsTrue(vex.ValidationResult.Errors.Where(x => x.Field == nameof(Member.MobileNumber)).Count() == 1);
            }
        }
    }
}