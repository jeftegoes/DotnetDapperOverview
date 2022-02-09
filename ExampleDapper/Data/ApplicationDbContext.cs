using ExampleDapper.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleDapper.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Company { get; set; }
    }
}