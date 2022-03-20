using FluentValidation;
using SmartCharging.Application.Group.Commands;

namespace SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;

public class CreateChargeStationValidator : AbstractValidator<CreateChargeStationCommand>
{
    public CreateChargeStationValidator()
    {
    }
}