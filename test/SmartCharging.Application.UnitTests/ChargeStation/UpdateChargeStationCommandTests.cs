using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;
using SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Application.UnitTests.Group;
using SmartCharging.Core.Exceptions;

namespace SmartCharging.Application.UnitTests.ChargeStation.ChargeStation;

public class UpdateChargeStationCommandTests : ApplicationTests
{
    [TestCase("Group 1", 3, "ChargeStation 1")]
    public async Task UpdateChargeStationCommandTest(string groupName, int capacityInAmps, string chargeStationName)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var responseCreateGroup = await mediator.Send(new CreateGroupCommand(groupName, capacityInAmps));
        
        var createChargeStationCommand = new CreateChargeStationCommand(responseCreateGroup.Id)
        {
            Name = chargeStationName
        };

        var responseCreateChargeStation = await mediator.Send(createChargeStationCommand);

        var updateChargeStationCommand = new UpdateChargeStationCommand(responseCreateGroup.Id, responseCreateChargeStation.Id)
        {
            Name = createChargeStationCommand.Name + "Updated",
        };

        updateChargeStationCommand.Name.ShouldBe(createChargeStationCommand.Name + "Updated");
        
        var responseUpdateChargeStation = await mediator.Send(updateChargeStationCommand);

        responseUpdateChargeStation.ShouldBe(Unit.Value);
    }
    
    [TestCase(0, 0, "Charge Station 1")]
    public async Task UpdateChargeStationCommandNotFoundExceptionTest(int groupId, int chargeStationId, string chargeStationName)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var updateChargeStationCommand = new UpdateChargeStationCommand(groupId, chargeStationId)
        {
            Name = chargeStationName
        };
        
        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(updateChargeStationCommand));
        exception.Message.ShouldBe(ExceptionMessages.ChargeStationCouldNotBeFound);
    }

    [TestCase(1, 5, "ChargeStation 1")]
    [TestCase(2, 3, "ChargeStation 2")]
    public async Task UpdateChargeStationCommandValidatorValidTest(int groupId, int chargeStationId, string chargeStationName)
    {
        var updateChargeStationCommand = new UpdateChargeStationCommand(groupId, chargeStationId)
        {
            Name = chargeStationName
        };
        
        var updateChargeStationCommandValidator = new UpdateChargeStationCommandValidator();

        var validationResult = await updateChargeStationCommandValidator.ValidateAsync(updateChargeStationCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }

    [TestCase(1, "ChargeStation 1", 0)]
    [TestCase(1, "", 3)]
    public async Task UpdateChargeStationCommandValidatorInValidTest(int groupId, string groupName, int chargeStationId)
    {
        var updateChargeStationCommand = new UpdateChargeStationCommand(groupId, chargeStationId)
        {
            Name = groupName,
        };
        
        var updateChargeStationCommandValidator = new UpdateChargeStationCommandValidator();

        var validationResult = await updateChargeStationCommandValidator.ValidateAsync(updateChargeStationCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}