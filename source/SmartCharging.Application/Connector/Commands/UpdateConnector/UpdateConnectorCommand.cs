using MediatR;

namespace SmartCharging.Application.Connector.Commands.UpdateConnector;

public class UpdateConnectorCommand : IRequest
{
    public UpdateConnectorCommand(int byGroupId, int byChargeStationId, int byConnectorId)
    {
        byGroupId = byGroupId;
        byChargeStationId = byChargeStationId;
        byConnectorId = byConnectorId;
    }
    
    // Where

    public int byGroupId { get; private set; }
    public int byChargeStationId { get; private set; }
    public int byConnectorId { get; private set; }
    
    // Update
    
    public int MaxCurrentInAmps  { get; set; }
}