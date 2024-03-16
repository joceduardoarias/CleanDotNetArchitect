using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;

public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
{
    public CreateDirectorCommandValidator()
    {
        RuleFor(p => p.Name)
           .NotNull().WithMessage("{Nombre} no puede estar en blanco");
           

        RuleFor(p => p.LastName)
            .NotNull().WithMessage("{LastName} no puede estar en blanco");
        
        RuleFor(p => p.VideoId)
            .NotEmpty().WithMessage("{VideoId} no puede estar en blanco")
            .NotNull();
    }
}

