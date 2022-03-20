using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class UpdateChargeStationCommand : IRequest
{
    public UpdateChargeStationCommand(int filterGroupId, int filterChargeStationId)
    {
        FilterGroupId = filterGroupId;
        FilterChargeStationId = filterChargeStationId;
    }

    // Filter

    public int FilterGroupId { get; private set; }
    public int FilterChargeStationId { get; private set; }
    
    // Update
    
    public string Name { get; set; }
    public int GroupId { get; set; }
}