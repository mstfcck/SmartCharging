using MediatR;

namespace SmartCharging.Application.Group.Commands.UpdateGroup;

public class UpdateGroupCommand : IRequest
{
    public UpdateGroupCommand(int byGroupId)
    {
        byGroupId = byGroupId;
    }

    // Where
    
    public int byGroupId { get; private set; }
    
    // Update
    
    public string Name { get; set; }
    public int CapacityInAmps  { get; set; }
}