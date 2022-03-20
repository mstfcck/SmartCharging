using FluentValidation;

namespace SmartCharging.Application.Group.Commands.CreateGroup;

public class CreateGroupCommandValidator: AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
    }
}