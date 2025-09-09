namespace Libry.Domain.Entities;

/// <remarks>
/// https://schema.org/Person
/// </remarks>
public sealed class Author : BaseEntity
{
    public string GivenName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string AdditionalName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public List<Book> Books { get; set; } = [];
}
