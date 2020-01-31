using System.Linq;
using WonderAPI.Pkg;
using WonderAPI.Pkg.Model;

namespace WonderAPI.Controllers.Account
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
            db.Members.Add(member);
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
            var member = db.Members.Where(m => m.Email == email).FirstOrDefault();
            return member;
        }

        /// <summary>
        /// Return an existing member by its ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public Member GetById(int memberID)
        {
            var member = db.Members.Find(memberID);
            return member;
        }

        /// <summary>
        /// Updates an existing member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member Update(Member member)
        {
            var existingMember = db.Members.Find(member.ID);

            existingMember.Name = member.Name;
            existingMember.Email = member.Name;
            existingMember.OptionalEmail = member.Name;
            existingMember.MobileNumber = member.Name;
            existingMember.Gender = member.Gender;
            existingMember.DateOfBirth = member.DateOfBirth;

            db.SaveChanges();

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
