using Libry.Domain.Dtos;
using Libry.Domain.Entities;

namespace Libry.Infrastructure.Mappers;

public static class AuthorMappers
{
    public static List<AuthorDto> MapToDto(this List<Author> authors)
    {
        return [.. authors.Select(a => a.MapToDto())];
    }

    public static AuthorDto MapToDto(this Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            GivenName = author.GivenName,
            FamilyName = author.FamilyName,
            AdditionalName = author.AdditionalName,
            BirthDate = author.BirthDate,
        };
    }
}
