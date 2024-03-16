using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTest.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTest.Features.Streamers.Command.DeleteStreamer;

public class DeleteStreamerCommandHandlerXUnitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;
    private readonly Mock<ILogger<DeleteStreamerCommandHandler>> _logger;
    public DeleteStreamerCommandHandlerXUnitTests()
    {
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();

        var mapperConfing = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfing.CreateMapper();

        _logger = new Mock<ILogger<DeleteStreamerCommandHandler>>();

        MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task DeleteStreamerCommand_InputStreamer_ReturnsUnit()
    {
        var deleteStreamerCommand = new DeleteStreamerCommand
        {
            Id = 3
        };

        var handler = new DeleteStreamerCommandHandler(_unitOfWork.Object, _mapper,_logger.Object);

        var result = await handler.Handle(deleteStreamerCommand, CancellationToken.None);

        result.ShouldBeOfType<Unit>();
    }
}
