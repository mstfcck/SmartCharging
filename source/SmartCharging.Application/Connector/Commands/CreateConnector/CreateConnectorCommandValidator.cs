using FluentValidation;

namespace SmartCharging.Application.Connector.Commands.CreateConnector;

public class CreateConnectorCommandValidator : AbstractValidator<CreateConnectorCommand>
{
    public CreateConnectorCommandValidator()
    {
    }
}