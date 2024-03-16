using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
{
    public CreateStreamerCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
            .NotNull()
            .MaximumLength(50).WithMessage("{Nombre} no puede estar en blanco");

        RuleFor(p => p.Url)
            .NotEmpty().WithMessage("La {url} no puede estar en blanco");
    }
}
