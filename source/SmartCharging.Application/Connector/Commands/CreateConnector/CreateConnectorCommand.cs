using MediatR;

namespace SmartCharging.Application.Connector.Commands.CreateConnector;

public class CreateConnectorCommand : IRequest
{
    public CreateConnectorCommand(int maxCurrentInAmps, int chargeStationId)
    {
        MaxCurrentInAmps = maxCurrentInAmps;
        ChargeStationId = chargeStationId;
    }
    
    public int MaxCurrentInAmps  { get; private set; }
    public int ChargeStationId { get; private set; }
}