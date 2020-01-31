using Microsoft.EntityFrameworkCore; 

namespace WonderAPI.Entities
{
    public class WonderDBContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}
