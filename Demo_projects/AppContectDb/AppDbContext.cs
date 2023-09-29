using Demo_projects.Models;
using Microsoft.EntityFrameworkCore;


namespace Demo_projects.AppContectDb
{
    public class AppDbContext : DbContext
    {
        public DbSet<Billing> Billings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=billing.db");
        }
    }
}
