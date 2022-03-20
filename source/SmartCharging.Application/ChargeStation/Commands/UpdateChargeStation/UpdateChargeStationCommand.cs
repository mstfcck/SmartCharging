using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class UpdateChargeStationCommand : IRequest
{
    public UpdateChargeStationCommand(int byGroupId, int byChargeStationId)
    {
        ByGroupId = byGroupId;
        ByChargeStationId = byChargeStationId;
    }

    // Filter

    public int ByGroupId { get; private set; }
    public int ByChargeStationId { get; private set; }
    
    // Update
    
    public string Name { get; set; }
    public int GroupId { get; set; }
}