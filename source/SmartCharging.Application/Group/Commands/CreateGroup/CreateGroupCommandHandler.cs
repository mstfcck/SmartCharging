using MediatR;

namespace SmartCharging.Application.Group.Commands;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand>
{
    public CreateGroupCommandHandler()
    {
    }

    public Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}