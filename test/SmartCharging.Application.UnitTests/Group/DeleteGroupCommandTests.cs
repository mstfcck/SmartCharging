using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.Group.Commands.CreateGroup;
using SmartCharging.Application.Group.Commands.DeleteGroup;
using SmartCharging.Core.Exceptions;

namespace SmartCharging.Application.UnitTests.Group;

[TestFixture, Order(3)]
public class DeleteGroupCommandTests : ApplicationTests
{
    [TestCase("Group 1", 5)]
    [TestCase("Group 2", 3)]
    public async Task DeleteGroupCommandTest(string groupName, int capacityInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var responseCreateGroup = await mediator.Send(new CreateGroupCommand(groupName, capacityInAmps));

        responseCreateGroup.ShouldNotBeNull();

        var deleteGroupCommand = new DeleteGroupCommand(responseCreateGroup.Id);

        var responseDeleteGroup = await mediator.Send(deleteGroupCommand);

        responseDeleteGroup.ShouldBe(Unit.Value);
    }

    [TestCase(0)]
    public async Task DeleteGroupCommandGroupCouldNotBeFoundExceptionTest(int groupId)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var deleteGroupCommand = new DeleteGroupCommand(0);
        
        var exception = await Should.ThrowAsync<BusinessException>(() => mediator.Send(deleteGroupCommand));
        exception.Message.ShouldBe(ExceptionMessages.GroupCouldNotBeFound);
    }

    [TestCase(1)]
    public async Task DeleteGroupCommandValidatorValidTest(int groupId)
    {
        var deleteGroupCommand = new DeleteGroupCommand(groupId);

        var deleteGroupCommandValidator = new DeleteGroupCommandValidator();

        var validationResult = await deleteGroupCommandValidator.ValidateAsync(deleteGroupCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }

    [TestCase(0)]
    public async Task DeleteGroupCommandValidatorInValidTest(int groupId)
    {
        var deleteGroupCommand = new DeleteGroupCommand(groupId);

        var deleteGroupCommandValidator = new DeleteGroupCommandValidator();

        var validationResult = await deleteGroupCommandValidator.ValidateAsync(deleteGroupCommand);

        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}