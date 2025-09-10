using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Libry.Api.Attributes;
using Libry.Domain.Dtos;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static Libry.Api.Defaults;

namespace Libry.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(CacheProfileName = CacheProfileName)]
public sealed class BooksController(
    ILibryRepository libryRepository)
    : ControllerBase
{
    private readonly ILibryRepository _libryRepository = libryRepository;

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<List<Book>>> GetAllAsync(Guid pageFromId, [Range(1, 1000)] int pageSize = PageSize)
    {
        var results = await _libryRepository.GetAllBooksAsync(pageFromId, pageSize);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }

    [HttpGet("{bookId}")]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Book>> GetById([Required] Guid bookId)
    {
        var result = await _libryRepository.GetBookByIdAsync(bookId);

        return result.IsSuccess
            ? Ok(result.Data)
            : NotFound();
    }

    [HttpGet("{bookId}/Authors")]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Book>> GetBookAuthorsAsync([Required] Guid bookId)
    {
        var result = await _libryRepository.GetBookAuthorsAsync(bookId);

        return result.IsSuccess
            ? Ok(result.Data)
            : NotFound();
    }

    [HttpPost]
    [ResponseCache(NoStore = true)]
    [ValidateModel]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Book>> AddAsync(Book book)
    {
        var result = await _libryRepository.AddBookAsync(book);

        Response.Headers.Add("location", new Uri($"{Request.GetEncodedUrl()}/{result.Data.Id}").ToString());

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error.Description);
    }
}
