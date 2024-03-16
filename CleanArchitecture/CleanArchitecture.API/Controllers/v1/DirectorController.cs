using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class DirectorController : ControllerBase
{
    private readonly IMediator _mediator;

    public DirectorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "CreateDirector")]
    [Authorize(Roles = "administrator")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> Create([FromBody] CreateDirectorCommand command)
    {
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
}
