using Libry.Domain.Dtos;
using Libry.Domain.Entities;
using Libry.Domain.Result;
using Libry.Infrastructure.Mappers;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Libry.Infrastructure.Repositories;

public sealed class LibryRepository(
    DbContext dbContext,
    ILogger<ILibryRepository> logger)
    : ILibryRepository
{
    private readonly DbContext _dbContext = dbContext;
    private readonly ILogger<ILibryRepository> _logger = logger;

    // Authors
    public async Task<Result<List<AuthorDto>>> GetAllAuthorsAsync(Guid pageFrom, int pageSize)
    {
        var authors = await _dbContext.Authors
            .Where(a => a.Id.CompareTo(pageFrom) > 0)
            .Take(pageSize)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return authors.IsNullOrEmpty()
            ? Result<List<AuthorDto>>.Failure(Errors.NotFound)
            : Result<List<AuthorDto>>.Success(authors.MapToDto());
    }

    public async Task<Result<AuthorDto>> GetAuthorByIdAsync(Guid fromId)
    {
        var author = await _dbContext.Authors
            .Where(a => a.Id == fromId)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return author is null
           ? Result<AuthorDto>.Failure(Errors.NotFound)
           : Result<AuthorDto>.Success(author.MapToDto());
    }

    public async Task<Result<List<BookDto>>> GetAuthorBooksAsync(Guid bookId)
    {
        var books = await _dbContext.Books
            .Where(b => b.Authors.Select(b => b.Id).Contains(bookId))
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return books.IsNullOrEmpty()
            ? Result<List<BookDto>>.Failure(Errors.NotFound)
            : Result<List<BookDto>>.Success(books.MapToDto());
    }

    // Books
    public async Task<Result<List<BookDto>>> GetAllBooksAsync(Guid fromId, int pageSize)
    {
        var books = await _dbContext.Books
            .Where(b => b.Id.CompareTo(fromId) > 0)
            .Take(pageSize)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return books.IsNullOrEmpty()
            ? Result<List<BookDto>>.Failure(Errors.NotFound)
            : Result<List<BookDto>>.Success(books.MapToDto());
    }

    public async Task<Result<BookDto>> GetBookByIdAsync(Guid id)
    {
        var book = await _dbContext.Books
            .Where(b => b.Id.Equals(id))
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .SingleOrDefaultAsync();

        return book is null
            ? Result<BookDto>.Failure(Errors.NotFound)
            : Result<BookDto>.Success(book.MapToDto());
    }

    public async Task<Result<List<AuthorDto>>> GetBookAuthorsAsync(Guid bookId)
    {
        var authors = await _dbContext.Authors
            .Where(a => a.Books.Select(b => b.Id).Contains(bookId))
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return authors.IsNullOrEmpty()
            ? Result<List<AuthorDto>>.Failure(Errors.NotFound)
            : Result<List<AuthorDto>>.Success(authors.MapToDto());
    }

    public async Task<Result<BookDto>> AddBookAsync(AddBookDto book)
    {
        var entity = new Book();
        try
        {
            var library = await _dbContext.Libraries.FirstOrDefaultAsync();

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
            return Result<BookDto>.Failure(Errors.InternalServerError);
        }

        return Result<BookDto>.Success(entity.MapToDto());
    }

    // Libraries
    public async Task<Result<List<LibraryDto>>> GetAllLibrariesAsync(Guid fromId, int pageSize)
    {
        var libraries = await _dbContext.Libraries
            .Where(b => b.Id.CompareTo(fromId) > 0)
            .Take(pageSize)
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .ToListAsync();

        return libraries.IsNullOrEmpty()
            ? Result<List<LibraryDto>>.Failure(Errors.NotFound)
            : Result<List<LibraryDto>>.Success(libraries.MapToDto());
    }
}
