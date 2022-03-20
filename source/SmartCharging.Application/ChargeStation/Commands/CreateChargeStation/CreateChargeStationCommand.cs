using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;

public class CreateChargeStationCommand : IRequest
{
    public CreateChargeStationCommand(int byGroupId)
    {
        ByGroupId = byGroupId;
    }
    
    // Where

    public int ByGroupId { get; private set; }
    
    // Create
    
    public string Name { get; set; }
}