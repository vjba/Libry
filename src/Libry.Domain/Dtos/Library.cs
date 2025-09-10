namespace Libry.Domain.Dtos;

public sealed record Library : Base
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
