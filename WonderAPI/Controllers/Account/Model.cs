using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// Represent Member business entity
    /// </summary>
    public class Member
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OptionalEmail { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


    /// <summary>
    /// Represent MemberInfo model, it's actually a Member model, but without password
    /// </summary>
    public class MemberInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string OptionalEmail { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
