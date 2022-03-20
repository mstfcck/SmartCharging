using FluentValidation;

namespace SmartCharging.Application.Connector.Commands.UpdateConnector;

public class UpdateConnectorCommandValidator : AbstractValidator<UpdateConnectorCommand>
{
    public UpdateConnectorCommandValidator()
    {
        RuleFor(x => x.ByGroupId)
            .GreaterThan(0);

        RuleFor(x => x.ByChargeStationId)
            .GreaterThan(0);

        RuleFor(x => x.ByConnectorId)
            .GreaterThan(0);

        RuleFor(x => x.MaxCurrentInAmps)
            .GreaterThan(0);
    }
}