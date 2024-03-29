﻿using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;

public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand, Unit>
{
    //private readonly IStreamerRepository _streamRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteStreamerCommandHandler> _logger;

    public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteStreamerCommandHandler> logger)
    {
        //_streamRepository = streamRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        //var streamerToDelete = await _streamRepository.GetByIdAsync(request.Id);

        var streamerToDelete = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);

        if (streamerToDelete == null)
        {
            _logger.LogError($"No se econtro el streamer id {request.Id}");
            throw new NotFoundException(nameof(Streamer), request.Id);
        }
        
        _mapper.Map(request, streamerToDelete, typeof(UpdateStreamerCommand), typeof(Streamer));

        //await _streamRepository.DeleteAsync(streamerToDelete);

        _unitOfWork.StreamerRepository.DeleteEntity(streamerToDelete);
        
        await _unitOfWork.Complete();

        _logger.LogInformation($"Streamer fue eliminado exitosamente");

        return Unit.Value;
    }
}
