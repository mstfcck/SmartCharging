using FluentValidation;

namespace SmartCharging.Application.Group.Commands.UpdateGroup;

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        
        RuleFor(x => x.CapacityInAmps)
            .GreaterThan(0);
    }
}