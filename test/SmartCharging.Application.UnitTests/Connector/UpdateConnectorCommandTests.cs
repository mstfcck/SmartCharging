using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;
using SmartCharging.Application.Connector.Commands.CreateConnector;
using SmartCharging.Application.Connector.Commands.UpdateConnector;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Core.Exceptions;

namespace SmartCharging.Application.UnitTests.Connector;

[TestFixture, Order(2)]
public class UpdateConnectorCommandTests : ApplicationTests
{
    [Test, Order(1)]
    [TestCase("Group 1", 5, "Charge Station 1", 1)]
    [TestCase("Group 2", 3, "Charge Station 2", 1)]
    public async Task UpdateConnectorCommandTest(string groupName, int capacityInAmps, string chargeStationName, int maxCurrentInAmps)
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

        var updateConnectorCommand = new UpdateConnectorCommand(responseCreateGroup.Id, responseCreateChargeStation.Id, responseCreateConnector.Id)
        {
            MaxCurrentInAmps = maxCurrentInAmps + 1
        };

        updateConnectorCommand.MaxCurrentInAmps.ShouldBe(maxCurrentInAmps + 1);
        
        var responseUpdateConnector = await mediator.Send(updateConnectorCommand);

        responseUpdateConnector.ShouldBe(Unit.Value);
    }
    
    [Test, Order(2)]
    [TestCase(0, 0, 0, 5)]
    public async Task UpdateConnectorCommandConnectorNotFoundExceptionTest(int groupId, int chargeStationId, int connectorId, int maxCurrentInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var updateConnectorCommand = new UpdateConnectorCommand(groupId, chargeStationId, connectorId)
        {
            MaxCurrentInAmps = maxCurrentInAmps
        };
        
        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(updateConnectorCommand));
        exception.Message.ShouldBe(ExceptionMessages.ConnectorCouldNotBeFound);
    }
    
    [Test, Order(3)]
    [TestCase("Group 1", 5, "Charge Station 1", 4, 6)]
    public async Task UpdateConnectorCommandGroupCapacityIsNotEnoughExceptionTest(string groupName, int capacityInAmps, string chargeStationName, int maxCurrentInAmps, int updatedMaxCurrentInAmps)
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

        var updateConnectorCommand = new UpdateConnectorCommand(responseCreateGroup.Id, responseCreateChargeStation.Id, responseCreateConnector.Id)
        {
            MaxCurrentInAmps = updatedMaxCurrentInAmps
        };

        updateConnectorCommand.MaxCurrentInAmps.ShouldBe(updatedMaxCurrentInAmps);
        
        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(updateConnectorCommand));
        exception.Message.ShouldBe(ExceptionMessages.GroupCapacityIsNotEnough);
    }

    [Test, Order(4)]
    [TestCase(1, 5, 1, 5)]
    [TestCase(1, 5, 2, 5)]
    public async Task UpdateConnectorCommandValidatorValidTest(int groupId, int chargeStationId, int connectorId, int maxCurrentInAmps)
    {
        var updateConnectorCommand = new UpdateConnectorCommand(groupId, chargeStationId, connectorId)
        {
            MaxCurrentInAmps = maxCurrentInAmps
        };
        
        var updateConnectorCommandValidator = new UpdateConnectorCommandValidator();

        var validationResult = await updateConnectorCommandValidator.ValidateAsync(updateConnectorCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }

    [Test, Order(5)]
    [TestCase(0, 0, 0, 0)]
    public async Task UpdateConnectorCommandValidatorInValidTest(int groupId, int chargeStationId, int connectorId, int maxCurrentInAmps)
    {
        var updateConnectorCommand = new UpdateConnectorCommand(groupId, chargeStationId, connectorId)
        {
            MaxCurrentInAmps = maxCurrentInAmps
        };
        
        var updateConnectorCommandValidator = new UpdateConnectorCommandValidator();

        var validationResult = await updateConnectorCommandValidator.ValidateAsync(updateConnectorCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}