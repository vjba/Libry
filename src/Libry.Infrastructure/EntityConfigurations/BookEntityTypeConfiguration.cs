using Libry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libry.Infrastructure.EntityConfigurations;

public sealed class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");

        // props
        builder.Property(b => b.Id)
            .IsRequired();

        // indexes
        builder
            .HasIndex(b => b.Id)
            .IsUnique()
            .HasDatabaseName("IX_BookIds_Descending")
            .IsDescending();

        builder
            .HasIndex(b => b.Title)
            .HasDatabaseName("IX_BookTitles_Descending")
            .IsDescending();

        // relationships
    }
}
