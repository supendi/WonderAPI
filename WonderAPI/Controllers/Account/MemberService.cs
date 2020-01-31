using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// IMemberRepository is an interface for working with data storage such as SQL server
    /// </summary>
    public interface IMemberRepository
    {
        /// <summary>
        /// Add a new member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Member Add(Member member);

        /// <summary>
        /// Updates an existing member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Member Update(Member member);


        /// <summary>
        /// Get member info by member ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        MemberInfo GetById(string memberID);

        /// <summary>
        /// Get member info by email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        MemberInfo GetByEmail(string email);
    }


    /// <summary>
    /// This exception will be thrown if duplicate email found on registration or update member
    /// </summary>
    public class DuplicateEmailException : AppException
    {
        public DuplicateEmailException(string msg) : base(msg)
        {
        }
    }

    public class MemberService
    {
        IMemberRepository memberRepository;

        /// <summary>
        /// Provide the Member business functionality based on requirement such as create, update and get member info.
        /// </summary>
        /// <param name="memberRepository"></param>
        public MemberService(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        /// <summary>
        /// Register a new member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public MemberInfo RegisterNewMember(Member member)
        {
            var retrievedUser = memberRepository.GetByEmail(member.Email);
            if (retrievedUser != null && retrievedUser.Email == member.Email)
                throw new DuplicateEmailException($"Email '{member.Email}' already registerd");

            var newMember = memberRepository.Add(member);
            return new MemberInfo
            {
                Name = newMember.Name,
                Email = newMember.Email,
                OptionalEmail = newMember.OptionalEmail,
                MobileNumber = newMember.MobileNumber,
                DateOfBirth = newMember.DateOfBirth,
                Gender = newMember.Gender
            };
        }
    }
}
