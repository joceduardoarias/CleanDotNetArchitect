using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamCommandHandler : IRequestHandler<CreateStreamerCommand, int>
{
    //private readonly IStreamerRepository _streamRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateStreamCommandHandler> _logger;

    public CreateStreamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateStreamCommandHandler> logger)
    {
        //_streamRepository = streamRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerEntity = _mapper.Map<Streamer>(request);

        if (streamerEntity == null)
        {
            return 0;
        }

        //var newStreamer = await _streamRepository.AddAsync(streamerEntity);
        
        _unitOfWork.StreamerRepository.AddEntity(streamerEntity);
        
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            throw new Exception($"No se pudo insertar el record de streamer");
        }

        _logger.LogInformation($"Streamer {streamerEntity.Id} fue creado exiosamente");

        return streamerEntity.Id;
    }
}
