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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<FilterValue> FilterValues { get; set; }
        public DbSet<PersonConnection> PersonConnections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PersonConnection> ProductConnections { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagConnection> TagConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasOne(t => t.User)
                .WithOne(e => e.Person)
                .HasForeignKey<Person>(t => t.UserId);

            modelBuilder.Entity<Person>()
                .HasMany(t => t.ConnectedPersons)
                .WithOne(e => e.FromPerson)
                .HasForeignKey(e => e.FromPersonId);

            modelBuilder.Entity<Person>()
                .HasMany(t => t.ConnectedTags)
                .WithOne(e => e.ConnectedTo as Person)
                .HasForeignKey(e => e.ConnectToId);

            modelBuilder.Entity<Person>()
                .HasMany(t => t.LikedProducts)
                .WithOne(e => e.PersonFrom)
                .HasForeignKey(e => e.PersonFromId);

            modelBuilder.Entity<Person>()
                .HasMany(t => t.LikedForPersonProducts)
                .WithOne(e => e.PersonTo)
                .HasForeignKey(e => e.PersonToId);

            modelBuilder.Entity<Product>()
                .HasMany(t => t.ConnectedTags)
                .WithOne(e => e.ConnectedTo as Product)
                .HasForeignKey(e => e.ConnectToId);

            modelBuilder.Entity<Product>()
                .HasMany(t => t.FilterValues)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Filter>()
                .HasMany(t => t.FilterValues)
                .WithOne(e => e.Filter)
                .HasForeignKey(e => e.FilterId);



        }


    }
}