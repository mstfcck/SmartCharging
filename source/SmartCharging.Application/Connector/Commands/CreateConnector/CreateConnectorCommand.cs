using MediatR;

namespace SmartCharging.Application.Connector.Commands.CreateConnector;

public class CreateConnectorCommand : IRequest
{
    public CreateConnectorCommand(int byGroupId, int byChargeStationId)
    {
        byGroupId = byGroupId;
        byChargeStationId = byChargeStationId;
    }
    
    // Where

    public int byGroupId { get; set; }
    public int byChargeStationId { get; set; }
    
    // Update
    
    public int MaxCurrentInAmps  { get; set; }
}