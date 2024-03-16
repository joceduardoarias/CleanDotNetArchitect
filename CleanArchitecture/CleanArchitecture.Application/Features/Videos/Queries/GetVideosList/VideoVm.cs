using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class VideoVm
{
    public string? Name { get; set; }
    public int StreamerId { get; set; }
    public virtual Streamer? Streamer { get; set; }
    public virtual ICollection<Actor> Actors { get; set; }
    public virtual Director Director { get; set; }
}
