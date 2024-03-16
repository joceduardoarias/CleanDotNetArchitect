using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StreamerDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
        );

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IStreamerRepository, StreamerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
        
    }
}
