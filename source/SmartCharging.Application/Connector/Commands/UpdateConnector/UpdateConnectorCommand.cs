using MediatR;

namespace SmartCharging.Application.Connector.Commands.UpdateConnector;

public class UpdateConnectorCommand : IRequest
{
    public UpdateConnectorCommand(int filterGroupId, int filterChargeStationId, int filterConnectorId)
    {
        FilterGroupId = filterGroupId;
        FilterChargeStationId = filterChargeStationId;
        FilterConnectorId = filterConnectorId;
    }
    
    // Filter

    public int FilterGroupId { get; private set; }
    public int FilterChargeStationId { get; private set; }
    public int FilterConnectorId { get; private set; }
    
    // Update
    
    public int MaxCurrentInAmps  { get; set; }
    public int ChargeStationId { get;  set; }
}