using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTest.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTest.Features.Streamers.Command.UpdateStreamer;

public class UpdateStreamerCommandHandlerXUnitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;
    private readonly Mock<ILogger<UpdateStreamerCommandHandler>> _logger;

    public UpdateStreamerCommandHandlerXUnitTests()
    {
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();

        var mapperConfing = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfing.CreateMapper();

        _logger = new Mock<ILogger<UpdateStreamerCommandHandler>>();

        MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task UpdateStreamerCommand_InputStreamer_ReturnsUnit()
    {
        var streamerInput = new UpdateStreamerCommand
        {
            Id = 3,
            Name = "test",
            Url = "testUrl"
        };

        var handler = new UpdateStreamerCommandHandler(_unitOfWork.Object,_mapper,_logger.Object);

        var result = await handler.Handle(streamerInput,CancellationToken.None);

        result.ShouldBeOfType<Unit>();
    }
}
