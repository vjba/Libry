using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using Libry.Domain.Dtos;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Libry.Api.Constants;
using static Libry.Api.Defaults;

namespace Libry.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(CacheProfileName = "Default")]
public sealed class BooksController(
    ILibryRepository libryRepository,
    ILogger<BooksController> logger)
    : ControllerBase
{
    private readonly ILibryRepository _libryRepository = libryRepository;
    private readonly ILogger<BooksController> _logger = logger;

    [HttpGet]
    [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<List<BookDto>>> GetAllAsync(Guid fromId, [Range(1, 1000)] int pageSize = PageSize)
    {
        _logger.LogInformation("{method} called.", nameof(GetAllAsync));

        var results = await _libryRepository.GetAllBooksAsync(fromId, pageSize);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }

    [HttpGet("{bookId}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BookDto>> GetById(Guid bookId)
    {
        _logger.LogInformation("{method} called.", nameof(GetById));

        if (bookId == Guid.Empty)
        {
            return BadRequest(EmptyGuidMessage);
        }

        var result = await _libryRepository.GetBookByIdAsync(bookId);

        return result.IsSuccess
            ? Ok(result.Data)
            : NotFound();
    }

    [HttpGet("{bookId}/Authors")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BookDto>> GetBookAuthorsAsync(Guid bookId)
    {
        _logger.LogInformation("{method} called.", nameof(GetBookAuthorsAsync));

        if (bookId == Guid.Empty)
        {
            return BadRequest(EmptyGuidMessage);
        }

        var result = await _libryRepository.GetBookAuthorsAsync(bookId);

        return result.IsSuccess
            ? Ok(result.Data)
            : NotFound();
    }

    [HttpPost]
    [ResponseCache(NoStore = true)]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BookDto>> AddAsync(AddBookDto book)
    {
        _logger.LogInformation("{method} called.", nameof(AddAsync));

        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Bad request POSTed to {method}.", nameof(AddAsync));
            return BadRequest(ModelState);
        }

        var result = await _libryRepository.AddBookAsync(book);

        Response.Headers.Add("location", new Uri($"{Request.Host + Request.Path + "/" + result.Data.Id}").ToString());

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error.Description);
    }
}
