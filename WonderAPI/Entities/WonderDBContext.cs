using Microsoft.EntityFrameworkCore;

namespace WonderAPI.Entities
{
    public class WonderDBContext : DbContext
    {
        private string connString;
        public DbSet<Member> Member { get; set; }

        private string GetConnectionString()
        {
            if (string.IsNullOrEmpty(connString)) 
                connString = System.Environment.GetEnvironmentVariable("WonderDB");

            if (connString == null)
            {
                connString = @"Data Source=.\sqlexpress; Initial Catalog=Wonder; Integrated Security=true;";
            }
            return connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
               .UseSqlServer(GetConnectionString());
        }
    }
}
