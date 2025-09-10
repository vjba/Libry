namespace Libry.Domain.Dtos;

public sealed record Book : Base
{
    public string Title { get; set; } = string.Empty;
    public int NumberOfPages { get; set; }
    public DateOnly DatePublished { get; set; }
}
