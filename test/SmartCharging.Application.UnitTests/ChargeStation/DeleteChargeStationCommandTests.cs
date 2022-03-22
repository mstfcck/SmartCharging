using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;
using SmartCharging.Application.ChargeStation.Commands.DeleteChargeStation;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Core.Exceptions;

namespace SmartCharging.Application.UnitTests.Group.ChargeStation;

public class DeleteChargeStationCommandTests : ApplicationTests
{
    [TestCase("Group 1", 3, "ChargeStation 1")]
    [TestCase("Group 2", 5, "ChargeStation 2")]
    public async Task DeleteChargeStationCommandTest(string groupName, int capacityInAmps, string chargeStationName)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var responseCreateGroup = await mediator.Send(new CreateGroupCommand(groupName, capacityInAmps));

        var createChargeStationCommand = new CreateChargeStationCommand(responseCreateGroup.Id)
        {
            Name = chargeStationName
        };

        var responseCreateChargeStation = await mediator.Send(createChargeStationCommand);

        responseCreateChargeStation.ShouldNotBeNull();

        var deleteChargeStationCommand = new DeleteChargeStationCommand(responseCreateGroup.Id, responseCreateChargeStation.Id);

        var responseDeleteChargeStation = await mediator.Send(deleteChargeStationCommand);

        responseDeleteChargeStation.ShouldBe(Unit.Value);
    }

    [TestCase(0, 0)]
    public async Task DeleteChargeStationCommandNotFoundChargeStationExceptionTest(int groupId, int chargeStationId)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var deleteChargeStationCommand = new DeleteChargeStationCommand(groupId, chargeStationId);

        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(deleteChargeStationCommand));
        exception.Message.ShouldBe(ExceptionMessages.ChargeStationCouldNotBeFound);
    }

    [TestCase(1, 1)]
    public async Task DeleteChargeStationCommandValidatorValidTest(int groupId, int chargeStationId)
    {
        var deleteChargeStationCommand = new DeleteChargeStationCommand(groupId, chargeStationId);

        var deleteChargeStationCommandValidator = new DeleteChargeStationCommandValidator();

        var validationResult = await deleteChargeStationCommandValidator.ValidateAsync(deleteChargeStationCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }

    [TestCase(0, 0)]
    public async Task DeleteChargeStationCommandValidatorInValidTest(int groupId, int chargeStationId)
    {
        var deleteChargeStationCommand = new DeleteChargeStationCommand(groupId, chargeStationId);

        var deleteChargeStationCommandValidator = new DeleteChargeStationCommandValidator();

        var validationResult = await deleteChargeStationCommandValidator.ValidateAsync(deleteChargeStationCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}