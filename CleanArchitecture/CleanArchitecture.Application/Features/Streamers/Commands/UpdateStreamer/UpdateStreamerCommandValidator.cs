using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
{
    public UpdateStreamerCommandValidator()
    {
        
        RuleFor(s => s.Name)
            .NotEmpty()
            .NotNull().WithMessage("El {Name} no puede estar en blanco");
        RuleFor(s => s.Url)
            .NotEmpty()
            .NotNull().WithMessage("El {Url} no puede estar en blanco");
    }
}
