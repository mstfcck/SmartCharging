
using MediatR;

namespace SmartCharging.Application.Group.Commands.CreateGroup;

public class CreateGroupCommand : IRequest<CreateGroupDTO>
{
    public CreateGroupCommand(string name, int capacityInAmps)
    {
        Name = name;
        CapacityInAmps = capacityInAmps;
    }
    
    public string Name { get; private set; }
    public int CapacityInAmps  { get; private set; }
}

public class CreateGroupDTO
{
    public int Id { get; set; }
}