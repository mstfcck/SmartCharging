using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models.Requests;
using SmartCharging.Api.Models.Responses;
using SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;
using SmartCharging.Application.ChargeStation.Commands.DeleteChargeStation;
using SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("groups")]
public class ChargeStationsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ChargeStationsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Create a charge station.
    /// </summary>
    /// <param name="groupId">Group Id</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{groupId}/chargestations")]
    [ProducesResponseType(typeof(Response<CreateChargeStationResponse>), StatusCodes.Status200OK)]
    public async Task<Response<CreateChargeStationResponse>> CreateChargeStation(
        [FromRoute] int groupId,
        [FromBody] CreateChargeStationRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new CreateChargeStationCommand(groupId)
        {
            Name = request.Name
        };

        var result = await _mediator.Send(command, cancellationToken);
        
        return new Response<CreateChargeStationResponse>(new CreateChargeStationResponse(result.Id));
    }

    /// <summary>
    /// Update a charge station.
    /// </summary>
    /// <param name="groupId">Group Id</param>
    /// <param name="chargeStationId">Charge Station Id</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{groupId}/chargestations/{chargeStationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task UpdateChargeStation(
        [FromRoute] int groupId, 
        [FromRoute] int chargeStationId, 
        [FromBody] UpdateChargeStationRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateChargeStationCommand(groupId, chargeStationId)
        {
            Name = request.Name
        };

        await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Delete a charge station.
    /// </summary>
    /// <param name="groupId">Group Id</param>
    /// <param name="chargeStationId">Charge Station Id</param>
    /// <param name="cancellationToken"></param>
    [HttpDelete("{groupId}/chargestations/{chargeStationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteChargeStation(
        [FromRoute] int groupId, 
        [FromRoute] int chargeStationId, 
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteChargeStationCommand(groupId, chargeStationId), cancellationToken);
    }
}