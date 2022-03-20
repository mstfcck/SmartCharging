using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Commands.DeleteChargeStation;

public class DeleteChargeStationCommandValidator : AbstractValidator<DeleteChargeStationCommand>
{
    public DeleteChargeStationCommandValidator()
    {
        RuleFor(x => x.GroupId)
            .GreaterThan(0);

        RuleFor(x => x.ChargeStationId)
            .GreaterThan(0);
    }
}