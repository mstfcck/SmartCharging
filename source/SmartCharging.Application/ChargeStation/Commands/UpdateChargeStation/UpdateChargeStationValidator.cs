using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class UpdateChargeStationValidator : AbstractValidator<UpdateChargeStationCommand>
{
    public UpdateChargeStationValidator()
    {
    }
}