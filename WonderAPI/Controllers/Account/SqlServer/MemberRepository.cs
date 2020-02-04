using System.Linq;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account.SqlServer
{
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
}
