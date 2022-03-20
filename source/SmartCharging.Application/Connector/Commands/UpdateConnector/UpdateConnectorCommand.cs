using MediatR;

namespace SmartCharging.Application.Connector.Commands.UpdateConnector;

public class UpdateConnectorCommand : IRequest
{
    public UpdateConnectorCommand(int byGroupId, int byChargeStationId, int byConnectorId)
    {
        ByGroupId = byGroupId;
        ByChargeStationId = byChargeStationId;
        ByConnectorId = byConnectorId;
    }
    
    // Where

    public int ByGroupId { get; private set; }
    public int ByChargeStationId { get; private set; }
    public int ByConnectorId { get; private set; }
    
    // Update
    
    public int MaxCurrentInAmps  { get; set; }
}