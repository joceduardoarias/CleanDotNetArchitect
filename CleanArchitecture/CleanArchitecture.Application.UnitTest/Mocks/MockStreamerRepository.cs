using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Application.UnitTest.Mocks;

public static class MockStreamerRepository
{
    public static void AddDataStreamerRepository(StreamerDbContext streamerDbContextFake)
    {
        var fixture = new Fixture();
        //Omitri las relaciones entre las tablas
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var streamers = fixture.CreateMany<Streamer>().ToList();

        //Se agregra un record a la lista de streamers - con estos se pude probar el update y el delete
        streamers.Add(fixture.Build<Streamer>()
            .With(tr => tr.Id, 3)
            .Without(tr => tr.Videos)
            .Create()
            );

        //TODO Se debe agregar un video al fixture para que funcione el GetVideoListTest

        streamerDbContextFake.Streamers!.AddRange(streamers);
        streamerDbContextFake.SaveChanges();

    }
}
