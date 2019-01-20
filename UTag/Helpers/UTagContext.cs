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
        public DbSet<ProductConnection> ProductConnections { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTagConnections { get; set; }
        public DbSet<PersonTag> PersonTagConnections { get; set; }

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
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.PersonTags)
                .WithOne(e => e.Tag)
                .HasForeignKey(e => e.TagId);

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
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(t => t.Likes)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Tag>()
               .HasMany(t => t.ProductTags)
               .WithOne(e => e.Tag)
               .HasForeignKey(e => e.TagId);

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