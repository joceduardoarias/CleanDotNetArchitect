using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Infrastructure.Repositories;

public class StreamerRepository : RepositoryBase<Streamer>, IStreamRepository
{
    public StreamerRepository(StreamerDbContext context) : base(context)
    {
    }
}
