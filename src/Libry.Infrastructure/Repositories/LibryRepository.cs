using Libry.Domain;
using Libry.Domain.Dtos;
using Libry.Infrastructure.Mappers;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Libry.Infrastructure.Repositories;

public sealed class LibryRepository(DbContext dbContext, ILogger<ILibryRepository> logger) : ILibryRepository
{
    private readonly DbContext _dbContext = dbContext;
    private readonly ILogger<ILibryRepository> _logger = logger;

    // Authors
    public async Task<Result<List<Author>>> GetAllAuthorsAsync(Guid pageFromId, int pageSize)
    {
        var authors = await _dbContext.Authors
            .Where(a => a.Id.CompareTo(pageFromId) > 0)
            .Take(pageSize)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return authors.IsNullOrEmpty()
            ? Result<List<Author>>.Failure(Errors.NotFound)
            : Result<List<Author>>.Success(authors.MapToDto());
    }

    public async Task<Result<Author>> GetAuthorByIdAsync(Guid authorId)
    {
        var author = await _dbContext.Authors
            .Where(a => a.Id == authorId)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return author is null
           ? Result<Author>.Failure(Errors.NotFound)
           : Result<Author>.Success(author.MapToDto());
    }

    public async Task<Result<List<Book>>> GetAuthorBooksAsync(Guid authorId)
    {
        var books = await _dbContext.Books
            .Where(b => b.Authors.Select(b => b.Id).Contains(authorId))
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return books.IsNullOrEmpty()
            ? Result<List<Book>>.Failure(Errors.NotFound)
            : Result<List<Book>>.Success(books.MapToDto());
    }

    // Books
    public async Task<Result<List<Book>>> GetAllBooksAsync(Guid pageFromId, int pageSize)
    {
        var books = await _dbContext.Books
            .Where(b => b.Id.CompareTo(pageFromId) > 0)
            .Take(pageSize)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return books.IsNullOrEmpty()
            ? Result<List<Book>>.Failure(Errors.NotFound)
            : Result<List<Book>>.Success(books.MapToDto());
    }

    public async Task<Result<Book>> GetBookByIdAsync(Guid bookId)
    {
        var book = await _dbContext.Books
            .Where(b => b.Id.Equals(bookId))
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .SingleOrDefaultAsync();

        return book is null
            ? Result<Book>.Failure(Errors.NotFound)
            : Result<Book>.Success(book.MapToDto());
    }

    public async Task<Result<List<Author>>> GetBookAuthorsAsync(Guid bookId)
    {
        var authors = await _dbContext.Authors
            .Where(a => a.Books.Select(b => b.Id).Contains(bookId))
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return authors.IsNullOrEmpty()
            ? Result<List<Author>>.Failure(Errors.NotFound)
            : Result<List<Author>>.Success(authors.MapToDto());
    }

    public async Task<Result<Book>> AddBookAsync(Book book)
    {
        var entity = new Domain.Entities.Book();

        try
        {
            var library = await _dbContext.Libraries.FirstAsync();

            entity.Id = Guid.NewGuid();
            entity.DatePublished = book.DatePublished;
            entity.NumberOfPages = book.NumberOfPages;
            entity.Title = book.Title;
            entity.Library = library;

            await _dbContext.Books.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);
            return Result<Book>.Failure(Errors.InternalServerError);
        }

        return Result<Book>.Success(entity.MapToDto());
    }

    // Libraries
    public async Task<Result<List<Library>>> GetAllLibrariesAsync(Guid pageFromId, int pageSize)
    {
        var libraries = await _dbContext.Libraries
            .Where(b => b.Id.CompareTo(pageFromId) > 0)
            .Take(pageSize)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return libraries.IsNullOrEmpty()
            ? Result<List<Library>>.Failure(Errors.NotFound)
            : Result<List<Library>>.Success(libraries.MapToDto());
    }
}
