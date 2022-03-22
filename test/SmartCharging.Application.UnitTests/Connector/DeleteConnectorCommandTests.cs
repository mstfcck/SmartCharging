using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;
using SmartCharging.Application.Connector.Commands.CreateConnector;
using SmartCharging.Application.Connector.Commands.DeleteConnector;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Core.Exceptions;

namespace SmartCharging.Application.UnitTests.Connector;

[TestFixture, Order(3)]
public class DeleteConnectorCommandTests : ApplicationTests
{
    [Test, Order(1)]
    [TestCase("Group 1", 5, "Charge Station 1", 1)]
    [TestCase("Group 2", 3, "Charge Station 2", 1)]
    public async Task DeleteConnectorCommandTest(string groupName, int capacityInAmps, string chargeStationName, int maxCurrentInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var responseCreateGroup = await mediator.Send(new CreateGroupCommand(groupName, capacityInAmps));
        
        var createChargeStationCommand = new CreateChargeStationCommand(responseCreateGroup.Id)
        {
            Name = chargeStationName
        };

        createChargeStationCommand.Name.ShouldBe(chargeStationName);
        
        var responseCreateChargeStation = await mediator.Send(createChargeStationCommand);
        
        var createConnectorCommand = new CreateConnectorCommand(responseCreateGroup.Id, responseCreateChargeStation.Id)
        {
            MaxCurrentInAmps = maxCurrentInAmps
        };

        var responseCreateConnector = await mediator.Send(createConnectorCommand);

        var deleteConnectorCommand = new DeleteConnectorCommand(responseCreateGroup.Id, responseCreateChargeStation.Id, responseCreateConnector.Id);

        var responseDeleteConnector = await mediator.Send(deleteConnectorCommand);

        responseDeleteConnector.ShouldBe(Unit.Value);
    }
    
    [Test, Order(2)]
    [TestCase(0, 0, 0, 5)]
    public async Task DeleteConnectorCommandConnectorNotFoundExceptionTest(int groupId, int chargeStationId, int connectorId, int maxCurrentInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var deleteConnectorCommand = new DeleteConnectorCommand(groupId, chargeStationId, connectorId);
        
        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(deleteConnectorCommand));
        exception.Message.ShouldBe(ExceptionMessages.ConnectorCouldNotBeFound);
    }

    [Test, Order(3)]
    [TestCase(1, 5, 1, 5)]
    [TestCase(1, 5, 2, 5)]
    public async Task DeleteConnectorCommandValidatorValidTest(int groupId, int chargeStationId, int connectorId, int maxCurrentInAmps)
    {
        var deleteConnectorCommand = new DeleteConnectorCommand(groupId, chargeStationId, connectorId);
        
        var deleteConnectorCommandValidator = new DeleteConnectorCommandValidator();

        var validationResult = await deleteConnectorCommandValidator.ValidateAsync(deleteConnectorCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }

    [Test, Order(4)]
    [TestCase(0, 0, 0, 0)]
    public async Task DeleteConnectorCommandValidatorInValidTest(int groupId, int chargeStationId, int connectorId, int maxCurrentInAmps)
    {
        var deleteConnectorCommand = new DeleteConnectorCommand(groupId, chargeStationId, connectorId);
        
        var deleteConnectorCommandValidator = new DeleteConnectorCommandValidator();

        var validationResult = await deleteConnectorCommandValidator.ValidateAsync(deleteConnectorCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}