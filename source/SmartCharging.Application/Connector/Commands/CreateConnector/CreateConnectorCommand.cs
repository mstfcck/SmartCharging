using MediatR;

namespace SmartCharging.Application.Connector.Commands.CreateConnector;

public class CreateConnectorCommand : IRequest
{
    public CreateConnectorCommand(int byGroupId, int byChargeStationId)
    {
        ByGroupId = byGroupId;
        ByChargeStationId = byChargeStationId;
    }
    
    // Where

    public int ByGroupId { get; set; }
    public int ByChargeStationId { get; set; }
    
    // Update
    
    public int MaxCurrentInAmps  { get; set; }
}