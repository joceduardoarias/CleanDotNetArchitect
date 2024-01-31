using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IStreamRepository : IAsyncRepository<Streamer>
{
}
