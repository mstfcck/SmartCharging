using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.DeleteChargeStation;

public class DeleteChargeStationCommand : IRequest
{
    public DeleteChargeStationCommand(int groupId, int chargeStationId)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
    }
    
    public int GroupId { get; private set; }
    public int ChargeStationId { get; private set; }
}