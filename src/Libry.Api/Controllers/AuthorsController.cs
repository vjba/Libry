using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using Libry.Domain.Dtos;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Libry.Api.Constants;
using static Libry.Api.Defaults;

namespace Libry.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(CacheProfileName = "Default")]
public sealed class AuthorsController(
    ILibryRepository libryRepository,
    ILogger<AuthorsController> logger)
    : ControllerBase
{
    private readonly ILibryRepository _libryRepository = libryRepository;
    private readonly ILogger<AuthorsController> _logger = logger;

    [HttpGet]
    [ProducesResponseType(typeof(List<AuthorDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<List<AuthorDto>>> GetAllAsync(Guid pageFrom, [Range(1, 1000)] int pageSize = PageSize)
    {
        _logger.LogInformation("{method} called.", nameof(GetAllAsync));

        var results = await _libryRepository.GetAllAuthorsAsync(pageFrom, pageSize);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }

    [HttpGet("{authorId}")]
    [ProducesResponseType(typeof(List<AuthorDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<AuthorDto>> GetByIdAsync(Guid authorId)
    {
        _logger.LogInformation("{method} called.", nameof(GetByIdAsync));

        if (authorId == Guid.Empty)
        {
            return BadRequest(EmptyGuidMessage);
        }

        var results = await _libryRepository.GetAuthorByIdAsync(authorId);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }

    [HttpGet("{authorId}/Books")]
    [ProducesResponseType(typeof(List<AuthorDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<List<BookDto>>> GetAuthorBooksAsync(Guid authorId)
    {
        _logger.LogInformation("{method} called.", nameof(GetAuthorBooksAsync));

        if (authorId == Guid.Empty)
        {
            return BadRequest(EmptyGuidMessage);
        }

        var results = await _libryRepository.GetAuthorBooksAsync(authorId);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }
}
