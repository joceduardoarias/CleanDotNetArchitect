using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamCommandHandler : IRequestHandler<CreateStreamerCommand, int>
{
    private readonly IStreamRepository _streamRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateStreamCommandHandler> _logger;

    public CreateStreamCommandHandler(IStreamRepository streamRepository, IMapper mapper, ILogger<CreateStreamCommandHandler> logger)
    {
        _streamRepository = streamRepository;
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

        var newStreamer = await _streamRepository.AddAsync(streamerEntity);

        _logger.LogInformation($"Streamer {newStreamer.Id} fue creado exiosamente");

        return newStreamer.Id;
    }
}
