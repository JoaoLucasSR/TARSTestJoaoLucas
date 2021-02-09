using Microsoft.EntityFrameworkCore;
using TARSTestJoaoLucas.Models;

namespace TARSTestJoaoLucas.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Project> Projects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}