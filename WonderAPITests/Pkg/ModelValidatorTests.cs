using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WonderAPI.Entities;

namespace WonderAPI.Pkg.Tests
{
    [TestClass()]
    public class ModelValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var newMember = new Entities.Member()
            {
                Name = "John Doe",
                Email = "",
                OptionalEmail = "",
                DateOfBirth = DateTime.Parse("1999-01-01"),
                Gender = "Male",
                MobileNumber = "",
                Password = ""
            };
            try
            {
                ModelValidator.Validate(newMember);
                Assert.Fail("This text should never be printed, validate method should throw an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ValidationException);
                var validationEx = ex as ValidationException;
                Assert.IsTrue(validationEx.ValidationResult.Errors.Count == 3);

                //Validate where field error names are below
                Assert.IsTrue(validationEx.ValidationResult.Errors.Where(x => x.Field == nameof(Member.Email)).Count() == 1);
                Assert.IsTrue(validationEx.ValidationResult.Errors.Where(x => x.Field == nameof(Member.Password)).Count() == 1);
                Assert.IsTrue(validationEx.ValidationResult.Errors.Where(x => x.Field == nameof(Member.MobileNumber)).Count() == 1);

            }
        }
    }
}