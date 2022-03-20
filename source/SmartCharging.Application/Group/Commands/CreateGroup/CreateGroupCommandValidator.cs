using FluentValidation;

namespace SmartCharging.Application.Group.Commands.CreateGroup;

public class CreateGroupCommandValidator: AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        
        RuleFor(x => x.CapacityInAmps)
            .GreaterThan(0);
    }
}