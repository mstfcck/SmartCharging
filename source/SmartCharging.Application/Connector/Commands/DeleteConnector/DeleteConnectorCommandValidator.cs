using FluentValidation;
using SmartCharging.Application.Group.Commands.DeleteGroup;

namespace SmartCharging.Application.Connector.Commands.DeleteConnector;

public class DeleteConnectorCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteConnectorCommandValidator()
    {
        RuleFor(x => x.GroupId)
            .GreaterThan(0);
    }
}