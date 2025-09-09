namespace Libry.Domain.Entities;

/// <remarks>
/// https://schema.org/Book
/// </remarks>
public sealed class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int NumberOfPages { get; set; }
    public DateOnly DatePublished { get; set; }
    public List<Author> Authors { get; set; } = [];
    public Library Library { get; set; } = null!;
}

