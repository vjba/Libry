namespace Libry.Domain.Dtos;

public abstract record PaginationDto
{
    public Guid LastId { get; set; }
    public int PageSize { get; set; }
}
