using Microsoft.EntityFrameworkCore;
using UTag.Models;

namespace UTag.Helpers
{
    public class UTagContext : DbContext
    {
        public UTagContext(DbContextOptions<UTagContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}