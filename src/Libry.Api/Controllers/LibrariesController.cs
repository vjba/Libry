using System.ComponentModel.DataAnnotations;
using System.Net;
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
[ResponseCache(CacheProfileName = "Default")]
public sealed class LibrariesController(
    ILibryRepository libryRepository,
    ILogger<LibrariesController> logger) : ControllerBase
{
    private readonly ILibryRepository _libryRepository = libryRepository;
    private readonly ILogger<LibrariesController> _logger = logger;

    [HttpGet]
    [ProducesResponseType(typeof(LibraryDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<LibraryDto>>> GetAllAsync(Guid fromId, [Range(1, 1000)] int pageSize = PageSize)
    {
        _logger.LogInformation("{method} called.", nameof(GetAllAsync));

        var results = await _libryRepository.GetAllLibrariesAsync(fromId, pageSize);

        return results.IsSuccess
            ? Ok(results.Data)
            : NotFound();
    }
}
