using Libry.Domain.Dtos;
using Libry.Domain.Entities;

namespace Libry.Infrastructure.Mappers;

public static class LibraryMappers
{
    public static List<LibraryDto> MapToDto(this List<Library> libraries)
    {
        return [.. libraries.Select(l => l.MapToDto())];
    }

    public static LibraryDto MapToDto(this Library library)
    {
        return new LibraryDto
        {
            Id = library.Id,
            Name = library.Name,
            Address = library.Address,
        };
    }
}
