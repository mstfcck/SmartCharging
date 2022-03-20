using MediatR;

namespace SmartCharging.Application.Group.Commands.CreateGroup;

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