using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models.Requests;
using SmartCharging.Api.Models.Responses;
using SmartCharging.Application.Group.Commands;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("groups")]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CreateGroupResponse), StatusCodes.Status201Created)]
    public async Task CreateGroup(
        [FromBody] CreateGroupRequest request, 
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateGroupCommand(request.Name, request.CapacityInAmps), cancellationToken);
        
        // TODO: Status kodu dön
    }

    [HttpPut("{groupId}")]
    [ProducesResponseType(typeof(UpdateGroupResponse), StatusCodes.Status200OK)]
    public async Task<UpdateGroupResponse> UpdateGroup(
        [FromBody] UpdateGroupRequest request, 
        CancellationToken cancellationToken)
    {
        return new UpdateGroupResponse();
    }

    [HttpDelete("{groupId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteGroup(
        [FromRoute] int groupId, 
        CancellationToken cancellationToken)
    {
    }
}