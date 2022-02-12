using ExampleDapper.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleDapper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Ignore(t => t.Employee);

            modelBuilder.Entity<Employee>()
                .HasOne(c => c.Company).WithMany(c => c.Employee)
                .HasForeignKey(c => c.CompanyId);
        }
    }
}