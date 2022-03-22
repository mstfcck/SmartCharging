using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using SmartCharging.Application.Group.Commands.CreateGroup;

namespace SmartCharging.Application.UnitTests.Group;

public class CreateGroupCommandTests : ApplicationTests
{
    [TestCase("Group 1", 5)]
    [TestCase("Group 2", 3)]
    public async Task CreateGroupCommandTest(string groupName, int capacityInAmps)
    {
        var mediator = ServiceProvider.GetService<IMediator>();

        mediator.ShouldNotBeNull();

        var createGroupCommand = new CreateGroupCommand(groupName, capacityInAmps);

        createGroupCommand.Name.ShouldBe(groupName);
        createGroupCommand.CapacityInAmps.ShouldBe(capacityInAmps);
        
        var response = await mediator.Send(createGroupCommand);
        
        response.ShouldNotBeNull();
    }
    
    [TestCase("Group 1", 5)]
    [TestCase("Group 2", 3)]
    public async Task CreateGroupCommandValidatorValidTest(string groupName, int capacityInAmps)
    {
        var createGroupCommand = new CreateGroupCommand(groupName, capacityInAmps);

        var createGroupCommandValidator = new CreateGroupCommandValidator();

        var validationResult = await createGroupCommandValidator.ValidateAsync(createGroupCommand);
        
        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(true);
        validationResult.Errors.Count.ShouldBe(0);
    }
    
    [TestCase("Group 1", 0)]
    [TestCase("", 3)]
    public async Task CreateGroupCommandValidatorInValidTest(string groupName, int capacityInAmps)
    {
        var createGroupCommand = new CreateGroupCommand(groupName, capacityInAmps);

        var createGroupCommandValidator = new CreateGroupCommandValidator();

        var validationResult = await createGroupCommandValidator.ValidateAsync(createGroupCommand);
        
        validationResult.ShouldNotBeNull();
        validationResult.IsValid.ShouldBe(false);
        validationResult.Errors.Count.ShouldBeGreaterThan(0);
    }
}