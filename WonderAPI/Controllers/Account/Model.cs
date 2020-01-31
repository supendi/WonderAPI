using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WonderAPI.Controllers.Account
{
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
