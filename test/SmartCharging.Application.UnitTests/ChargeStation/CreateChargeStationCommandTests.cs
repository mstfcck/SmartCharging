using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;
using SmartCharging.Application.Group.Commands.CreateGroup;

namespace SmartCharging.Application.UnitTests.ChargeStation;

[TestFixture, Order(1)]
public class CreateChargeStationCommandTests : ApplicationTests
{
    [TestCase("Group 1", 5, "Charge Station 1")]
    [TestCase("Group 2", 3, "Charge Station 2")]
    public async Task CreateChargeStationCommandTest(string groupName, int capacityInAmps, string chargeStationName)
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
        
        var responseChargeStation = await mediator.Send(createChargeStationCommand);
        
        responseChargeStation.ShouldNotBeNull();
    }
    
    [TestCase(1, "Charge Station 1")]
    public async Task CreateChargeStationCommandValidatorValidTest(int groupId, string chargeStationName)
    {
        var createChargeStationCommand = new CreateChargeStationCommand(groupId)
        {
            Name = chargeStationName
        };

        var createChargeStationCommandValidator = new CreateChargeStationCommandValidator();

        var validationResult = await createChargeStationCommandValidator.ValidateAsync(createChargeStationCommand);
        
        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }
    
    [TestCase(0, "")]
    public async Task CreateChargeStationCommandValidatorInValidTest(int groupId, string chargeStationName)
    {
        var createChargeStationCommand = new CreateChargeStationCommand(groupId)
        {
            Name = chargeStationName
        };

        var createChargeStationCommandValidator = new CreateChargeStationCommandValidator();

        var validationResult = await createChargeStationCommandValidator.ValidateAsync(createChargeStationCommand);
        
        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}