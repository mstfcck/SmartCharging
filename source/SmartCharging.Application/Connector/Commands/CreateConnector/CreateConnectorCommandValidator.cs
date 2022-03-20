using FluentValidation;

namespace SmartCharging.Application.Connector.Commands.CreateConnector;

public class CreateConnectorCommandValidator : AbstractValidator<CreateConnectorCommand>
{
    public CreateConnectorCommandValidator()
    {
        RuleFor(x => x.ByGroupId)
            .GreaterThan(0);

        RuleFor(x => x.ByChargeStationId)
            .GreaterThan(0);

        RuleFor(x => x.MaxCurrentInAmps)
            .GreaterThan(0);
    }
}