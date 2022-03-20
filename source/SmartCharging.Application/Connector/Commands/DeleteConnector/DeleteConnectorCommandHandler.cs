using MediatR;

namespace SmartCharging.Application.Connector.Commands.DeleteConnector;

public class DeleteConnectorCommandHandler : IRequestHandler<DeleteConnectorCommand>
{
    public DeleteConnectorCommandHandler()
    {
    }
    
    public Task<Unit> Handle(DeleteConnectorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}