using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;
using SmartCharging.Application.Connector.Commands.CreateConnector;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Core.Exceptions;

namespace SmartCharging.Application.UnitTests.Connector;

[TestFixture, Order(1)]
public class CreateConnectorCommandTests : ApplicationTests
{
    [Test, Order(1)]
    [TestCase("Group 1", 5, "Charge Station 1", 5)]
    [TestCase("Group 2", 5, "Charge Station 2", 3)]
    public async Task CreateConnectorCommandTest(string groupName, int capacityInAmps, string chargeStationName, int maxCurrentInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var createGroupCommand = new CreateGroupCommand(groupName, capacityInAmps);

        createGroupCommand.Name.ShouldBe(groupName);
        createGroupCommand.CapacityInAmps.ShouldBe(capacityInAmps);
        
        var responseCreateGroup = await mediator.Send(createGroupCommand);
        
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

        createConnectorCommand.MaxCurrentInAmps.ShouldBeGreaterThan(0);
        
        var responseConnector = await mediator.Send(createConnectorCommand);
        
        responseConnector.ShouldNotBeNull();
    }
    
    [Test, Order(2)]
    [TestCase("Group 1", 2, "Charge Station 1", 5)]
    [TestCase("Group 2", 3, "Charge Station 2", 4)]
    public async Task CreateConnectorCommandCapacityIsNotEnoughExceptionTest(string groupName, int capacityInAmps, string chargeStationName, int maxCurrentInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var createGroupCommand = new CreateGroupCommand(groupName, capacityInAmps);

        createGroupCommand.Name.ShouldBe(groupName);
        createGroupCommand.CapacityInAmps.ShouldBe(capacityInAmps);
        
        var responseCreateGroup = await mediator.Send(createGroupCommand);
        
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

        createConnectorCommand.MaxCurrentInAmps.ShouldBeGreaterThan(0);
        
        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(createConnectorCommand));
        exception.Message.ShouldBe(ExceptionMessages.GroupCapacityIsNotEnough);
    }
    
    [Test, Order(3)]
    [TestCase(1, 1, 3)]
    public async Task CreateConnectorCommandValidatorValidTest(int groupId, int chargeStationId, int maxCurrentInAmps)
    {
        var createConnectorCommand = new CreateConnectorCommand(groupId, chargeStationId)
        {
            MaxCurrentInAmps = maxCurrentInAmps
        };

        var createConnectorCommandValidator = new CreateConnectorCommandValidator();

        var validationResult = await createConnectorCommandValidator.ValidateAsync(createConnectorCommand);
        
        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }
    
    [Test, Order(4)]
    [TestCase(0, 0, 0)]
    public async Task CreateConnectorCommandValidatorInValidTest(int groupId, int chargeStationId, int maxCurrentInAmps)
    {
        var createConnectorCommand = new CreateConnectorCommand(groupId, chargeStationId)
        {
            MaxCurrentInAmps = maxCurrentInAmps
        };

        var createConnectorCommandValidator = new CreateConnectorCommandValidator();

        var validationResult = await createConnectorCommandValidator.ValidateAsync(createConnectorCommand);
        
        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}