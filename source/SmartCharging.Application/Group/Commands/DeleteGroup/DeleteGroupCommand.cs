using MediatR;

namespace SmartCharging.Application.Group.Commands.DeleteGroup;

public class DeleteGroupCommand : IRequest
{
    public int GroupId { get; set; }
}