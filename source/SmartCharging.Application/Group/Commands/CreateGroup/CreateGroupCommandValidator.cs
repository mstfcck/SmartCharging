using FluentValidation;

namespace SmartCharging.Application.Group.Commands;

public class CreateGroupCommandValidator: AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
    }
}