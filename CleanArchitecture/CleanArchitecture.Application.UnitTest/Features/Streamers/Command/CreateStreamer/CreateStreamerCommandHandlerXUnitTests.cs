using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTest.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTest.Features.Streamers.Command.CreateStreamer;

public class CreateStreamerCommandHandlerXUnitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;
    private readonly Mock<ILogger<CreateStreamCommandHandler>> _logger;

    public CreateStreamerCommandHandlerXUnitTests()
    {   
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();
        
        var mapperConfing = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });
        
        _mapper = mapperConfing.CreateMapper();
                
        _logger = new Mock<ILogger<CreateStreamCommandHandler>>();

        MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task CreateStreamerCommand_InputStreamer_ReturnsNumber()
    {
        var streamerInput = new CreateStreamerCommand
        {
            Name = "jeam streaming",
            Url = "www.jeam.com.co"
        };

        var handler = new CreateStreamCommandHandler(_unitOfWork.Object,_mapper,_logger.Object);

        var result = await handler.Handle(streamerInput, CancellationToken.None);

        result.ShouldBeOfType<int>();
        
    }
}
