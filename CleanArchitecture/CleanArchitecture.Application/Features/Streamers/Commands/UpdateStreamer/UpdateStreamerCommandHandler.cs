using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand,Unit>
{
    private readonly IStreamerRepository _streamRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateStreamerCommandHandler> _logger;

    public UpdateStreamerCommandHandler(IStreamerRepository streamRepository, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
    {
        _streamRepository = streamRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToUpdate = await _streamRepository.GetByIdAsync(request.Id);

        if (streamerToUpdate == null)
        {
            _logger.LogError($"No se econtro el streamer id {request.Id}");
            throw new NotFoundException(nameof(Streamer), request.Id);            
        }
        
        _mapper.Map(request,streamerToUpdate,typeof(UpdateStreamerCommand),typeof(Streamer));

        await _streamRepository.UpdateAsync(streamerToUpdate);

        _logger.LogInformation($"Streamer fue modificado exitosamente");

        return Unit.Value;
    }
}
