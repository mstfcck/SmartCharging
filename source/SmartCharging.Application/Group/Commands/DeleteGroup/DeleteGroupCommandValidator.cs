using FluentValidation;

namespace SmartCharging.Application.Group.Commands.DeleteGroup;

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.GroupId)
            .GreaterThan(0);
    }
}