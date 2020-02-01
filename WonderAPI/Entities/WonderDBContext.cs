using Microsoft.EntityFrameworkCore;

namespace WonderAPI.Entities
{
    public class WonderDBContext : DbContext
    {
        private static string connString;
        public DbSet<Member> Member { get; set; }

        private string GetConnectionString()
        {
            if (string.IsNullOrEmpty(connString))
            {
                connString = System.Environment.GetEnvironmentVariable("WonderDB");

                if (string.IsNullOrEmpty(connString))
                {
                    connString = @"Data Source=.\sqlexpress; Initial Catalog=Wonder; Integrated Security=true;";
                }
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
