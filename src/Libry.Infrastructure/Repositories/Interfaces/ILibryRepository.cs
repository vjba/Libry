using Libry.Domain.Dtos;
using Libry.Domain.Result;

namespace Libry.Infrastructure.Repositories.Interfaces;

public interface ILibryRepository
{
    // Authors
    public Task<Result<List<AuthorDto>>> GetAllAuthorsAsync(Guid fromId, int pageSize);
    public Task<Result<AuthorDto>> GetAuthorByIdAsync(Guid fromId);
    public Task<Result<List<BookDto>>> GetAuthorBooksAsync(Guid fromId);

    // Books
    public Task<Result<List<BookDto>>> GetAllBooksAsync(Guid fromId, int pageSize);
    public Task<Result<BookDto>> GetBookByIdAsync(Guid id);
    public Task<Result<List<AuthorDto>>> GetBookAuthorsAsync(Guid id);
    public Task<Result<BookDto>> AddBookAsync(AddBookDto book);

    // Libraries
    public Task<Result<List<LibraryDto>>> GetAllLibrariesAsync(Guid fromId, int pageSize);

}
