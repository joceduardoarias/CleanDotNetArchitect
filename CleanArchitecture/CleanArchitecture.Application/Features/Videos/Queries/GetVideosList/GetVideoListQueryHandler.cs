using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<VideoVm>>
{
    //private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVideoListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        //_videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<VideoVm>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
    {
        //var videList = await _videoRepository.GetVideoByUserName(request.Username);        

        var videList = await _unitOfWork.VideoRepository.GetVideoByUserName(request.Username);

        return _mapper.Map<List<VideoVm>>(videList);
    }
}
