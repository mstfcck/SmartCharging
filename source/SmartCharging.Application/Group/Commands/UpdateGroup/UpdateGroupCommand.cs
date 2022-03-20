using MediatR;

namespace SmartCharging.Application.Group.Commands.UpdateGroup;

public class UpdateGroupCommand : IRequest
{
    public UpdateGroupCommand(int byGroupId)
    {
        ByGroupId = byGroupId;
    }

    // Where
    
    public int ByGroupId { get; private set; }
    
    // Update
    
    public string Name { get; set; }
    public int CapacityInAmps  { get; set; }
}