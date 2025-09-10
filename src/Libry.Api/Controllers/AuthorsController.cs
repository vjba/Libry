using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Libry.Domain.Dtos;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Libry.Api.Defaults;

namespace Libry.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(CacheProfileName = CacheProfileName)]
public sealed class AuthorsController(ILibryRepository libryRepository) : ControllerBase
{
    private readonly ILibryRepository _libryRepository = libryRepository;

    [HttpGet]
    [ProducesResponseType(typeof(List<Author>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<List<Author>>> GetAllAsync(Guid pageFromId, [Range(1, 1000)] int pageSize = PageSize)
    {
        var results = await _libryRepository.GetAllAuthorsAsync(pageFromId, pageSize);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }

    [HttpGet("{authorId}")]
    [ProducesResponseType(typeof(List<Author>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Author>> GetByIdAsync([Required] Guid authorId)
    {
        var results = await _libryRepository.GetAuthorByIdAsync(authorId);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }

    [HttpGet("{authorId}/Books")]
    [ProducesResponseType(typeof(List<Author>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<List<Book>>> GetAuthorBooksAsync([Required] Guid authorId)
    {
        var results = await _libryRepository.GetAuthorBooksAsync(authorId);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }
}
