using MediatR;

namespace SmartCharging.Application.Connector.Commands.DeleteConnector;

public class DeleteConnectorCommand : IRequest
{
    public DeleteConnectorCommand(int groupId, int chargeStationId, int connectorId)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ConnectorId = connectorId;
    }
    
    public int GroupId { get; private set; }
    public int ChargeStationId { get; private set; }
    public int ConnectorId { get; private set; }
}