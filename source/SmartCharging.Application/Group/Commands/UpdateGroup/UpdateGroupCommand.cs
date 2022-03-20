using MediatR;

namespace SmartCharging.Application.Group.Commands.UpdateGroup;

public class UpdateGroupCommand : IRequest
{
    public UpdateGroupCommand(int filterGroupId)
    {
        FilterGroupId = filterGroupId;
    }

    // Filter
    
    public int FilterGroupId { get; private set; }
    
    // Update
    
    public string Name { get; set; }
    public int CapacityInAmps  { get; set; }
}