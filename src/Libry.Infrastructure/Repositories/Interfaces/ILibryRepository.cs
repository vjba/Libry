using Libry.Domain;
using Libry.Domain.Dtos;

namespace Libry.Infrastructure.Repositories.Interfaces;

public interface ILibryRepository
{
    // Authors
    public Task<Result<List<Author>>> GetAllAuthorsAsync(Guid pageFromId, int pageSize);
    public Task<Result<Author>> GetAuthorByIdAsync(Guid authorId);
    public Task<Result<List<Book>>> GetAuthorBooksAsync(Guid authorId);

    // Books
    public Task<Result<List<Book>>> GetAllBooksAsync(Guid pageFromId, int pageSize);
    public Task<Result<Book>> GetBookByIdAsync(Guid bookId);
    public Task<Result<List<Author>>> GetBookAuthorsAsync(Guid bookId);
    public Task<Result<Book>> AddBookAsync(Book book);

    // Libraries
    public Task<Result<List<Library>>> GetAllLibrariesAsync(Guid pageFromId, int pageSize);

}
