using MediatR;

namespace SmartCharging.Application.Group.Commands.DeleteGroup;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
{
    public DeleteGroupCommandHandler()
    {
    }
    
    public Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}