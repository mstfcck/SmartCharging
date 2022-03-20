using MediatR;

namespace SmartCharging.Application.Connector.Commands.UpdateConnector;

public class UpdateConnectorCommandHandler : IRequestHandler<UpdateConnectorCommand>
{
    public UpdateConnectorCommandHandler()
    {
    }
    
    public Task<Unit> Handle(UpdateConnectorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}