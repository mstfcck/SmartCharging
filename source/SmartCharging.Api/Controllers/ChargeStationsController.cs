using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models.Requests;
using SmartCharging.Api.Models.Responses;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("groups")]
public class ChargeStationsController : ControllerBase
{
    public ChargeStationsController()
    {
    }
    
    [HttpPost("{groupId}/chargestations")]
    [ProducesResponseType(typeof(CreateChargeStationResponse), StatusCodes.Status201Created)]
    public async Task<CreateChargeStationResponse> CreateChargeStation(
        [FromBody] CreateChargeStationRequest request, 
        CancellationToken cancellationToken)
    {
        return new CreateChargeStationResponse();
    }

    [HttpPut("{groupId}/chargestations/{chargeStationId}")]
    [ProducesResponseType(typeof(UpdateChargeStationResponse), StatusCodes.Status200OK)]
    public async Task<UpdateChargeStationResponse> UpdateChargeStation(
        [FromRoute] int groupId, 
        [FromRoute] int chargeStationId, 
        [FromBody] UpdateChargeStationRequest request, 
        CancellationToken cancellationToken)
    {
        return new UpdateChargeStationResponse();
    }

    [HttpDelete("{groupId}/chargestations/{chargeStationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteChargeStation(
        [FromRoute] int groupId, 
        [FromRoute] int chargeStationId, 
        CancellationToken cancellationToken)
    {
    }
}