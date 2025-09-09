namespace Libry.Domain.Entities;

/// <remarks>
/// https://schema.org/Library
/// </remarks>
public sealed class Library : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<Book> Books { get; set; } = [];
}

