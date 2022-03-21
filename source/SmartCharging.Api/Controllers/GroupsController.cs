using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models.Requests;
using SmartCharging.Api.Models.Responses;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Application.Group.Commands.DeleteGroup;
using SmartCharging.Application.Group.Commands.UpdateGroup;

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
    
    /// <summary>
    /// Create a group.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    [HttpPost]
    [ProducesResponseType(typeof(Response<CreateGroupResponse>), StatusCodes.Status200OK)]
    public async Task<Response<CreateGroupResponse>> CreateGroup(
        [FromBody] CreateGroupRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateGroupCommand(request.Name, request.CapacityInAmps), cancellationToken);
        return new Response<CreateGroupResponse>(new CreateGroupResponse(result.Id));
    }

    /// <summary>
    /// Update a group.
    /// </summary>
    /// <param name="groupId">Group Id</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{groupId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task UpdateGroup(
        [FromRoute] int groupId,
        [FromBody] UpdateGroupRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateGroupCommand(groupId)
        {
            Name = request.Name,
            CapacityInAmps = request.CapacityInAmps
        };

        await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Delete a group.
    /// </summary>
    /// <param name="groupId">Group Id</param>
    /// <param name="cancellationToken"></param>
    [HttpDelete("{groupId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteGroup(
        [FromRoute] int groupId, 
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteGroupCommand(groupId), cancellationToken);
    }
}