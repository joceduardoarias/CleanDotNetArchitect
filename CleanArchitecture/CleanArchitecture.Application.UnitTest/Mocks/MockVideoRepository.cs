using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CleanArchitecture.Application.UnitTest.Mocks;

public static class MockVideoRepository
{
    public static void GetVideoRepository(StreamerDbContext streamerDbContextFake)
    {
        var fixture = new Fixture();
        //Omitri las relaciones entre las tablas
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var videos = fixture.CreateMany<Video>().ToList();

        //Se agregra un record a la lista de videos
        videos.Add(fixture.Build<Video>()
            .With(tr => tr.CreatedBy, "Amazon Prime")
            .Create()
            );
               

        streamerDbContextFake.Videos!.AddRange(videos);
        streamerDbContextFake.SaveChanges();
                                        
    }
}
