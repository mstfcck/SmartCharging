using MediatR;

namespace SmartCharging.Application.Group.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
{
    public UpdateGroupCommandHandler()
    {
    }
    
    public Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}