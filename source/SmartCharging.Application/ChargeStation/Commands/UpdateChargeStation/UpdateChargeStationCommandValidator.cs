using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class UpdateChargeStationCommandValidator : AbstractValidator<UpdateChargeStationCommand>
{
    public UpdateChargeStationCommandValidator()
    {
        RuleFor(x => x.ByGroupId)
            .GreaterThan(0);

        RuleFor(x => x.ByChargeStationId)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}