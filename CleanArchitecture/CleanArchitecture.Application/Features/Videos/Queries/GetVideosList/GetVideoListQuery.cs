using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideoListQuery : IRequest<List<VideoVm>>
{    
    public string Username { get; set; } = string.Empty;
    public GetVideoListQuery(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}
