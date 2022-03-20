using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class DeleteChargeStationCommandValidator : AbstractValidator<DeleteChargeStationCommand>
{
    public DeleteChargeStationCommandValidator()
    {
        RuleFor(x => x.ByGroupId)
            .GreaterThan(0);

        RuleFor(x => x.ByChargeStationId)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}