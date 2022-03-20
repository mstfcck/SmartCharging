
using MediatR;

namespace SmartCharging.Application.Group.Commands;

public class CreateGroupCommand : IRequest
{
    public CreateGroupCommand(string name, int capacityInAmps)
    {
        Name = name;
        CapacityInAmps = capacityInAmps;
    }
    
    public string Name { get; private set; }
    public int CapacityInAmps  { get; private set; }
}