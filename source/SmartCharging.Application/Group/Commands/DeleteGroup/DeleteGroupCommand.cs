using MediatR;

namespace SmartCharging.Application.Group.Commands.DeleteGroup;

public class DeleteGroupCommand : IRequest
{
    public DeleteGroupCommand(int groupId)
    {
        GroupId = groupId;
    }
    
    public int GroupId { get; private set; }
}