using Libry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libry.Infrastructure.EntityConfigurations;

public sealed class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Author");

        // props
        builder.Property(a => a.Id)
            .IsRequired();

        // indexes
        builder
            .HasIndex(a => a.Id)
            .IsUnique()
            .HasDatabaseName("IX_AuthorIds_Descending")
            .IsDescending();

        builder
            .HasIndex(a => new { a.FamilyName, a.GivenName })
            .HasDatabaseName("IX_AuthorNames_Descending")
            .IsDescending();

        // relationships
    }
}
