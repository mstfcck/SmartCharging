using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;

public class CreateChargeStationCommand : IRequest
{
    public CreateChargeStationCommand(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
}