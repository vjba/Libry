using Dtos = Libry.Domain.Dtos;
using Entities = Libry.Domain.Entities;

namespace Libry.Infrastructure.Mappers;

public static class BookMappers
{
    public static List<Dtos.Book> MapToDto(this List<Entities.Book> books)
    {
        return [.. books.Select(b => b.MapToDto())];
    }

    public static Dtos.Book MapToDto(this Entities.Book book)
    {
        return new Dtos.Book
        {
            Id = book.Id,
            Title = book.Title,
            NumberOfPages = book.NumberOfPages,
            DatePublished = book.DatePublished
        };
    }
}
