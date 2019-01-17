using Microsoft.EntityFrameworkCore;
using Ncx.Exam.Api.Models;

namespace Ncx.Exam.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Book> Book { get; set; }
    }
}