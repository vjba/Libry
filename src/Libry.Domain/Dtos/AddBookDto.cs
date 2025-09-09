using System.ComponentModel.DataAnnotations;

namespace Libry.Domain.Dtos;
public sealed record AddBookDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Range(1, 10_000)]
    public int NumberOfPages { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly DatePublished { get; set; }
}
