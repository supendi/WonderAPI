﻿using System.Collections.Generic;
using System.Linq;
using WonderAPI.Entities;
using WonderAPI.Pkg;

namespace WonderAPI.Controllers.Account
{
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
    /// MemberRepository implements IMemberRepository. It uses SQL Server as data storage
    /// </summary>
    public class MemberRepository : IMemberRepository
    {
        WonderDBContext db;
        public MemberRepository(WonderDBContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Insert a new member into database
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member Add(Member member)
        {
            db.Member.Add(member);
            db.SaveChanges();
            return member;
        }


        /// <summary>
        /// Retrieve an existing member by its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetByEmail(string email)
        {
            var member = db.Member.SingleOrDefault(m => m.Email == email);
            return member;
        }

        /// <summary>
        /// Return an existing member by its ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public Member GetById(int memberID)
        {
            var member = db.Member.Find(memberID);
            return member;
        }

        /// <summary>
        /// Updates an existing member, except password and email
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member Update(Member member)
        {
            var existingMember = db.Member.Find(member.ID);

            if (existingMember != null)
            {
                existingMember.Name = member.Name;
                existingMember.OptionalEmail = member.OptionalEmail;
                existingMember.MobileNumber = member.MobileNumber;
                existingMember.Gender = member.Gender;
                existingMember.DateOfBirth = member.DateOfBirth;

                db.SaveChanges();
            }

            return existingMember;
        }

        /// <summary>
        /// Dispose the db context inside.
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }

    /// <summary>
    /// In memory repository member storage
    /// </summary>
    public class MemberInmemRepository : IMemberRepository
    {
        private List<Member> members;

        /// <summary>
        /// Get last ID, or current ID. Ordered by ID desc
        /// </summary>
        /// <returns></returns>
        private int GetLastID()
        {
            if (members == null || members.Count == 0)
            {
                return 0;
            }
            return members.OrderByDescending(x => x.ID).FirstOrDefault().ID;
        }

        /// <summary>
        /// Get next auto ID
        /// </summary>
        /// <returns></returns>
        private int GetNextID()
        {
            return GetLastID() + 1;
        }

        /// <summary>
        /// Create new inmem instance with initial data
        /// </summary>
        /// <param name="members"></param>
        public MemberInmemRepository(List<Member> members)
        {
            this.members = members;
        }

        /// <summary>
        /// Add new member into memory
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member Add(Member member)
        {
            member.ID = GetNextID();
            this.members.Add(member);
            return member;
        }

        /// <summary>
        /// Get member by its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetByEmail(string email)
        {
            if (members == null || members.Count == 0)
            {
                return null;
            }
            return members.FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Get member by its ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public Member GetById(int memberID)
        {
            if (members == null || members.Count == 0)
            {
                return null;
            }
            return members.FirstOrDefault(x => x.ID == memberID);
        }

        /// <summary>
        /// Updates existing member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member Update(Member member)
        {
            foreach (var element in members)
            {
                if (element.ID == member.ID)
                {
                    element.Name = member.Name;
                    element.OptionalEmail = member.Name;
                    element.MobileNumber = member.Name;
                    element.DateOfBirth = member.DateOfBirth;
                    element.Gender = member.Gender;
                    return element;
                }
            }
            return null;
        }

        /// <summary>
        /// Dispose data
        /// </summary>
        public void Dispose()
        {
            this.members = null;
        }
    }
}
