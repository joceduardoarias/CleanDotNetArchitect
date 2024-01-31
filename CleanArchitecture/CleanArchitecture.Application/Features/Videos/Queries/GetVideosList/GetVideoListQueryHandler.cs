using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<VideoVm>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IMapper _mapper;

    public GetVideoListQueryHandler(IVideoRepository videoRepository, IMapper mapper)
    {
        _videoRepository = videoRepository;
        _mapper = mapper;
    }

    public async Task<List<VideoVm>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
    {
        var videList = await _videoRepository.GetVideoByUserName(request.Username);        
        
        return _mapper.Map<List<VideoVm>>(videList);
    }
}
