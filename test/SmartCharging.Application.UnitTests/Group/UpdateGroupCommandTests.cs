using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Application.Group.Commands.UpdateGroup;
using SmartCharging.Core.Exceptions;

namespace SmartCharging.Application.UnitTests.Group;

[TestFixture, Order(2)]
public class UpdateGroupCommandTests : ApplicationTests
{
    [TestCase("Group 1", 5)]
    [TestCase("Group 2", 3)]
    public async Task UpdateGroupCommandTest(string groupName, int capacityInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var createGroupCommand = new CreateGroupCommand(groupName, capacityInAmps);

        var responseCreateGroup = await mediator.Send(createGroupCommand);

        responseCreateGroup.ShouldNotBeNull();

        var updateGroupCommand = new UpdateGroupCommand(responseCreateGroup.Id)
        {
            Name = createGroupCommand.Name + "Updated",
            CapacityInAmps = createGroupCommand.CapacityInAmps + 1
        };

        updateGroupCommand.Name.ShouldBe(createGroupCommand.Name + "Updated");
        updateGroupCommand.CapacityInAmps.ShouldBe(createGroupCommand.CapacityInAmps + 1);
        
        var responseUpdateGroup = await mediator.Send(updateGroupCommand);

        responseUpdateGroup.ShouldBe(Unit.Value);
    }
    
    [TestCase(0, "Group 1", 5)]
    public async Task UpdateGroupCommandGroupCouldNotBeFoundExceptionTest(int groupId, string groupName, int capacityInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var updateGroupCommand = new UpdateGroupCommand(groupId)
        {
            Name = groupName,
            CapacityInAmps = capacityInAmps
        };
        
        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(updateGroupCommand));
        exception.Message.ShouldBe(ExceptionMessages.GroupCouldNotBeFound);
    }

    [TestCase(1, "Group 1", 5)]
    [TestCase(1, "Group 2", 3)]
    public async Task UpdateGroupCommandValidatorValidTest(int groupId, string groupName, int capacityInAmps)
    {
        var updateGroupCommand = new UpdateGroupCommand(groupId)
        {
            Name = groupName,
            CapacityInAmps = capacityInAmps
        };
        
        var updateGroupCommandValidator = new UpdateGroupCommandValidator();

        var validationResult = await updateGroupCommandValidator.ValidateAsync(updateGroupCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }

    [TestCase(1, "Group 1", 0)]
    [TestCase(1, "", 3)]
    public async Task UpdateGroupCommandValidatorInValidTest(int groupId, string groupName, int capacityInAmps)
    {
        var updateGroupCommand = new UpdateGroupCommand(groupId)
        {
            Name = groupName,
            CapacityInAmps = capacityInAmps
        };
        
        var updateGroupCommandValidator = new UpdateGroupCommandValidator();

        var validationResult = await updateGroupCommandValidator.ValidateAsync(updateGroupCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}