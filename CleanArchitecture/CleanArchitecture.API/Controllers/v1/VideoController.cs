using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VideoController : ControllerBase
{
    private readonly IMediator _mediator;

    public VideoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userName}", Name = "GetVideo")]
    [ProducesResponseType(typeof(IEnumerable<VideoVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<VideoVm>>> GetVideoByUserName(string userName)
    {
        var query = new GetVideoListQuery(userName);

        var result = await _mediator.Send(query);

        return Ok(result);
    }

}
