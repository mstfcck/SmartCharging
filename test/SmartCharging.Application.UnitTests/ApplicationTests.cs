using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SmartCharging.Infrastructure;
using SmartCharging.Infrastructure.Database;

namespace SmartCharging.Application.UnitTests.Group;

[TestFixture]
public class ApplicationTests
{
    public ServiceProvider ServiceProvider { get; set; }

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(Bootstrapper));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("SmartCharging"));
        
        services.AddInfrastructure();

        ServiceProvider = services.BuildServiceProvider();
    }
}