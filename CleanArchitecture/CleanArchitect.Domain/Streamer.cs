
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain;

public class Streamer : BaseDomainModel
{
    public Streamer()
    {
        Videos = new HashSet<Video>();
    }

    public string? Name { get; set; }
    public string? Url { get; set;}    
    public ICollection<Video> Videos { get; set; }
}
