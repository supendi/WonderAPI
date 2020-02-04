using System.Collections.Generic;
using System.Linq;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account.Inmem
{
    /// <summary>
    /// In memory member repository which use memory as storage.
    /// </summary>
    public class MemberRepository : IMemberRepository
    {
        public List<Member> Members { get; set; }

        /// <summary>
        /// Create new inmem instance with initial data
        /// </summary>
        /// <param name="members"></param>
        public MemberRepository(List<Member> members)
        {
            this.Members = members;
        }

        /// <summary>
        /// Get last ID, or current ID. Will return the biggest number
        /// </summary>
        /// <returns></returns>
        private int GetLastID()
        {
            if (Members == null || Members.Count == 0)
            {
                return 0;
            }
            return Members.OrderByDescending(x => x.ID).FirstOrDefault().ID;
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
        /// Add new member into memory
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member Add(Member member)
        {
            member.ID = GetNextID();
            this.Members.Add(member);
            return member;
        }

        /// <summary>
        /// Get member by its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetByEmail(string email)
        {
            if (Members == null || Members.Count == 0)
            {
                return null;
            }
            return Members.FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Get member by its ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public Member GetById(int memberID)
        {
            if (Members == null || Members.Count == 0)
            {
                return null;
            }
            return Members.FirstOrDefault(x => x.ID == memberID);
        }

        /// <summary>
        /// Updates existing member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member Update(Member member)
        {
            foreach (var element in Members)
            {
                if (element.ID == member.ID)
                {
                    element.Name = member.Name;
                    element.OptionalEmail = member.OptionalEmail;
                    element.MobileNumber = member.MobileNumber;
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
            this.Members = null;
        }
    }
}
