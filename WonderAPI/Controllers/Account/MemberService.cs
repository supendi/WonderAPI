using System;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// This exception will be thrown if duplicate email found while registering or updating a member
    /// </summary>
    public class DuplicateEmailException : AppException
    {
        public DuplicateEmailException(string email) : base($"Email '{email}' already registered")
        {
        }
    }

    /// <summary>
    /// This exception will be thrown if user is not found
    /// </summary>
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException() : base($"User is not found")
        {
        }
    }

    /// <summary>
    /// IMemberRepository is an interface for working with data storage such as SQL server
    /// </summary>
    public interface IMemberRepository : IRepository
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
        Member GetById(int memberID);

        /// <summary>
        /// Get member info by email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Member GetByEmail(string email);
    }

    /// <summary>
    /// Provide the Member business functionality based on requirement such as create, update and get member info.
    /// It implements IDisposable, the main reason is to dispose the repository object.
    /// </summary>
    public class MemberService : IDisposable
    {
        IMemberRepository memberRepository;
        IPasswordHasher passwordHasher;

        public MemberService(IMemberRepository memberRepository, IPasswordHasher passwordHasher)
        {
            this.memberRepository = memberRepository;
            this.passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Register a new member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member RegisterNewMember(Member member)
        {
            var retrievedMember = memberRepository.GetByEmail(member.Email);
            if (retrievedMember != null && retrievedMember.Email == member.Email)
                throw new DuplicateEmailException(member.Email);

            member.Password = passwordHasher.Hash(member.Password);

            return memberRepository.Add(member);
        }

        /// <summary>
        /// Updates an existing member, but not including its password and its email
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        public Member UpdateMember(MemberUpdateRequest updateRequest)
        {
            var existingMember = memberRepository.GetById(updateRequest.ID);
            if (existingMember == null)
                throw new UserNotFoundException();

            var member = new Member
            {
                ID = updateRequest.ID,
                Name = updateRequest.Name,
                OptionalEmail = updateRequest.OptionalEmail,
                MobileNumber = updateRequest.MobileNumber,
                Gender = updateRequest.Gender,
                DateOfBirth = updateRequest.DateOfBirth
            };
            return memberRepository.Update(member);
        }

        /// <summary>
        /// Return member info by using its ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public Member GetMember(int memberID)
        {
            var existingMember = memberRepository.GetById(memberID);
            if (existingMember == null)
                throw new UserNotFoundException();

            return existingMember;
        }

        /// <summary>
        /// This dispose will call the repository dispose method
        /// </summary>
        public void Dispose()
        {
            memberRepository.Dispose();
        }
    }
}
