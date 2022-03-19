using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models.Requests;
using SmartCharging.Api.Models.Responses;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("groups")]
public class ConnectorsController : ControllerBase
{
    public ConnectorsController()
    {
    }

    [HttpPost("{groupId}/chargestations{connectorId}/connectors")]
    [ProducesResponseType(typeof(CreateConnectorResponse), StatusCodes.Status201Created)]
    public async Task<CreateConnectorResponse> CreateConnector(
        [FromBody] CreateConnectorRequest request,
        CancellationToken cancellationToken)
    {
        return new CreateConnectorResponse();
    }

    [HttpPut("{groupId}/chargestations/{chargeStationId}/connectors/{connectorId}")]
    [ProducesResponseType(typeof(UpdateConnectorResponse), StatusCodes.Status200OK)]
    public async Task<UpdateConnectorResponse> UpdateConnector(
        [FromRoute] int groupId,
        [FromRoute] int chargeStationId,
        [FromRoute] int connectorId,
        [FromBody] UpdateConnectorRequest request,
        CancellationToken cancellationToken)
    {
        return new UpdateConnectorResponse();
    }

    [HttpDelete("{groupId}/chargestations/{chargeStationId}/connectors/{connectorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteConnector(
        [FromRoute] int groupId,
        [FromRoute] int chargeStationId,
        [FromRoute] int connectorId,
        CancellationToken cancellationToken)
    {
    }
}