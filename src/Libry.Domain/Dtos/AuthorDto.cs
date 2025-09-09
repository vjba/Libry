namespace Libry.Domain.Dtos;

public sealed record AuthorDto : BaseDto
{
    public string GivenName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string AdditionalName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
}
