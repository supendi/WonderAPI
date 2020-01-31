using Microsoft.EntityFrameworkCore;
using WonderAPI.Pkg.Model;

namespace WonderAPI.Pkg
{
    public class WonderDBContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}
