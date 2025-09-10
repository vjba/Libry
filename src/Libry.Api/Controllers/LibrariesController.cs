using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Libry.Domain.Dtos;
using Libry.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Libry.Api.Defaults;

namespace Libry.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ResponseCache(CacheProfileName = CacheProfileName)]
public sealed class LibrariesController(
    ILibryRepository libryRepository,
    ILogger<LibrariesController> logger)
    : ControllerBase
{
    private readonly ILibryRepository _libryRepository = libryRepository;
    private readonly ILogger<LibrariesController> _logger = logger;

    [HttpGet]
    [ProducesResponseType(typeof(Library), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<List<Library>>> GetAllAsync(Guid pageFromId, [Range(1, 1000)] int pageSize = PageSize)
    {
        var results = await _libryRepository.GetAllLibrariesAsync(pageFromId, pageSize);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }
}
