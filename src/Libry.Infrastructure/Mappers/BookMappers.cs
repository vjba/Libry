using Libry.Domain.Dtos;
using Libry.Domain.Entities;

namespace Libry.Infrastructure.Mappers;

public static class BookMappers
{
    public static List<BookDto> MapToDto(this List<Book> books)
    {
        return [.. books.Select(b => b.MapToDto())];
    }

    public static BookDto MapToDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            NumberOfPages = book.NumberOfPages,
            DatePublished = book.DatePublished
        };
    }
}
