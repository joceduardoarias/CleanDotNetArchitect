using AutoMapper;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTest.Mocks;
using Moq;
using Xunit;
using Shouldly;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Application.UnitTest.Features.Video.Queries;

public class GetVideoListQueryHandlerXunitTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;

    public GetVideoListQueryHandlerXunitTests()
    {
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();
        var mapperConfing = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfing.CreateMapper();
        MockVideoRepository.GetVideoRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task GetVideoListTest()
    {
        var handler = new GetVideoListQueryHandler(_unitOfWork.Object, _mapper);
        var request = new GetVideoListQuery("Amazon Prime");
        
        var result = await handler.Handle(request, CancellationToken.None);

        result.ShouldBeOfType<List<VideoVm>>();

        result.Count.ShouldBe(1);
    }
}
