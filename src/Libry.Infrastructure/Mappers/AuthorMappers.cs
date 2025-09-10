using Dtos = Libry.Domain.Dtos;
using Entities = Libry.Domain.Entities;

namespace Libry.Infrastructure.Mappers;

public static class AuthorMappers
{
    public static List<Dtos.Author> MapToDto(this List<Entities.Author> authors)
    {
        return [.. authors.Select(a => a.MapToDto())];
    }

    public static Dtos.Author MapToDto(this Entities.Author author)
    {
        return new Dtos.Author
        {
            Id = author.Id,
            GivenName = author.GivenName,
            FamilyName = author.FamilyName,
            AdditionalName = author.AdditionalName,
            BirthDate = author.BirthDate,
        };
    }
}
