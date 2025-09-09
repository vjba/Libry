using Libry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libry.Infrastructure.EntityConfigurations;

public sealed class LibraryEntityTypeConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Library");

        // props
        builder
            .Property(l => l.Id)
            .IsRequired();

        // indexes
        builder
            .HasIndex(l => l.Id)
            .IsUnique()
            .HasDatabaseName("IX_LibraryIds_Descending")
            .IsDescending();

        builder
            .HasIndex(l => l.Name)
            .HasDatabaseName("IX_LibraryNames_Descending")
            .IsDescending();

        // relationships
    }
}
