﻿using System;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// This exception will be thrown if duplicate email found while registering or updating a member
    /// </summary>
    public class DuplicateEmailException : AppException
    {
        public DuplicateEmailException(string msg) : base(msg)
        {
        }
    }

    /// <summary>
    /// This exception will be thrown if user is not found
    /// </summary>
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException(string msg) : base(msg)
        {
        }
    }


    /// <summary>
    /// This exeption will be thrown if user failed to login
    /// </summary>
    public class AuthenticationException : AppException
    {
        public AuthenticationException(string msg) : base(msg)
        {
        }
    }

    /// <summary>
    /// Provide the Member business functionality based on requirement such as create, update and get member info.
    /// </summary>
    public class MemberService : IDisposable
    {
        IMemberRepository memberRepository;
        IPasswordHasher passwordHasher;
        ITokenGenerator tokenGenerator;

        public MemberService(IMemberRepository memberRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            this.memberRepository = memberRepository;
            this.passwordHasher = passwordHasher;
            this.tokenGenerator = tokenGenerator;
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
                throw new DuplicateEmailException($"Email '{member.Email}' already registered");

            member.Password = passwordHasher.Hash(member.Password);

            return memberRepository.Add(member);
        }

        /// <summary>
        /// Updates an existing member, but not including its password
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member UpdateMember(Member member)
        {
            var existingMember = memberRepository.GetById(member.ID);
            if (existingMember == null)
                throw new UserNotFoundException($"User is not found");

            var retrievedMember = memberRepository.GetByEmail(member.Email);
            if (retrievedMember != null && retrievedMember.ID == member.ID)
                throw new DuplicateEmailException($"Email '{member.Email}' is duplicate");

            return memberRepository.Update(member);
        }

        /// <summary>
        /// Authenticates user by providing its email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthInfo Authenticate(string email, string password)
        {
            var member = memberRepository.GetByEmail(email);
            if (member == null)
                throw new AuthenticationException("Invalid email or password.");

            if (member.Password != passwordHasher.Hash(password))
                throw new AuthenticationException("Invalid email or password.");

            var newToken = tokenGenerator.Generate(member);
            return new AuthInfo
            {
                Token = newToken
            };
        }

        /// <summary>
        /// Return member info by using its ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public MemberInfo GetMemberInfo(int memberID)
        {
            var existingMember = memberRepository.GetById(memberID);
            if (existingMember == null)
                throw new UserNotFoundException($"User is not found");

            return new MemberInfo
            {
                Name = existingMember.Name,
                Email = existingMember.Name,
                OptionalEmail = existingMember.Name,
                MobileNumber = existingMember.Name,
                Gender = existingMember.Gender,
                DateOfBirth = existingMember.DateOfBirth
            };
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
