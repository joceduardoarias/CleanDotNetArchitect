using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class VideoRepository : RepositoryBase<Video>, IVideoRepository
{
    public VideoRepository(StreamerDbContext context) : base(context)
    {
    }

    public async Task<Video> GetVideoByName(string name)
    {
        var video = await _context.Videos!.Where(v => v.Name == name).FirstOrDefaultAsync();

        return video;
    }

    public async Task<IEnumerable<Video>> GetVideoByUserName(string username)
    {
        var video = await _context.Videos!.Where(v => v.Streamer!.Name == username).ToListAsync();

        return video;
    }
}
