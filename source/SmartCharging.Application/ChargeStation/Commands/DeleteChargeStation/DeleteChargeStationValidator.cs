using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Commands.DeleteChargeStation;

public class DeleteChargeStationValidator : AbstractValidator<DeleteChargeStationCommand>
{
    public DeleteChargeStationValidator()
    {
    }
}