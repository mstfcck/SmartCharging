using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;

public class CreateChargeStationCommandValidator : AbstractValidator<CreateChargeStationCommand>
{
    public CreateChargeStationCommandValidator()
    {
        RuleFor(x => x.ByGroupId)
            .GreaterThan(0);
        
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}