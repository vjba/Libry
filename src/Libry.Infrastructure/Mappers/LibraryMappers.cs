using Dtos = Libry.Domain.Dtos;
using Entities = Libry.Domain.Entities;

namespace Libry.Infrastructure.Mappers;

public static class LibraryMappers
{
    public static List<Dtos.Library> MapToDto(this List<Entities.Library> libraries)
    {
        return [.. libraries.Select(l => l.MapToDto())];
    }

    public static Dtos.Library MapToDto(this Entities.Library library)
    {
        return new Dtos.Library
        {
            Id = library.Id,
            Name = library.Name,
            Address = library.Address,
        };
    }
}
