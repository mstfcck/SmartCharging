using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class DeleteChargeStationCommand : IRequest
{
    public DeleteChargeStationCommand(int byGroupId, int byChargeStationId)
    {
        ByGroupId = byGroupId;
        ByChargeStationId = byChargeStationId;
    }

    // Where

    public int ByGroupId { get; private set; }
    public int ByChargeStationId { get; private set; }
    
    // Update
    
    public string Name { get; set; }
}