using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Api.Models.Requests;
using SmartCharging.Api.Models.Responses;
using SmartCharging.Application.Connector.Commands.CreateConnector;
using SmartCharging.Application.Connector.Commands.DeleteConnector;
using SmartCharging.Application.Connector.Commands.UpdateConnector;

namespace SmartCharging.Api.Controllers;

[ApiController]
[Route("groups")]
public class ConnectorsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ConnectorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{groupId}/chargestations/{chargeStationId}/connectors")]
    [ProducesResponseType(typeof(CreateConnectorResponse), StatusCodes.Status201Created)]
    public async Task<CreateConnectorResponse> CreateConnector(
        [FromRoute] int groupId,
        [FromRoute] int chargeStationId,
        [FromBody] CreateConnectorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateConnectorCommand(groupId, chargeStationId)
        {
            MaxCurrentInAmps = request.MaxCurrentInAmps
        };

        await _mediator.Send(command, cancellationToken);
        
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
        var command = new UpdateConnectorCommand(groupId, chargeStationId, connectorId)
        {
            MaxCurrentInAmps = request.MaxCurrentInAmps
        };

        await _mediator.Send(command, cancellationToken);
        
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
        await _mediator.Send(new DeleteConnectorCommand(groupId, chargeStationId, connectorId), cancellationToken);
    }
}