using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models.Requests;
using SmartCharging.Api.Models.Responses;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("groups")]
public class GroupsController : ControllerBase
{
    public GroupsController()
    {
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(CreateGroupResponse), StatusCodes.Status201Created)]
    public async Task<CreateGroupResponse> CreateGroup(
        [FromBody] CreateGroupRequest request, 
        CancellationToken cancellationToken)
    {
        return new CreateGroupResponse();
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