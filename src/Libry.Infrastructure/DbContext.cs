using Libry.Domain.Entities;
using Libry.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Libry.Infrastructure;

public class DbContext(IConfiguration configuration) : Microsoft.EntityFrameworkCore.DbContext
{
    private IConfiguration Configuration { get; set; } = configuration;
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
        new AuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<Author>());
        new LibraryEntityTypeConfiguration().Configure(modelBuilder.Entity<Library>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder
            .UseSqlServer(Configuration.GetConnectionString("Database"))
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine)
            .UseSeeding((context, _) =>
            {
                var seeder = new DatabaseSeeder();
                context.Set<Book>().AddRange(seeder.Books);
                context.Set<Author>().AddRange(seeder.Authors);
                context.Set<Library>().AddRange(seeder.Library);

                context.SaveChanges();
            });
}
