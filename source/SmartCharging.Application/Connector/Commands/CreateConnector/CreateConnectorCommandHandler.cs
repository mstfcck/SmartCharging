using MediatR;

namespace SmartCharging.Application.Connector.Commands.CreateConnector;

public class CreateConnectorCommandHandler : IRequestHandler<CreateConnectorCommand>
{
    public CreateConnectorCommandHandler()
    {
    }

    public Task<Unit> Handle(CreateConnectorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}