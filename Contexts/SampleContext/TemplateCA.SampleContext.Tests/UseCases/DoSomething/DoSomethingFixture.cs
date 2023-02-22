using Microsoft.Extensions.DependencyInjection;
using TemplateCA.SampleContext.Applications.DoSomething;
using TemplateCA.SampleContext.Domain;
using TemplateCA.SharedKernel.Testing.Fixtures;

namespace TemplateCA.SampleContext.Tests.UseCases.DoSomething;

public class DoSomethingFixture : CommandFixture
{
    private DoSomethingCommand command = new(1);

    public DoSomethingFixture TryToAccessNegativeId()
    {
        this.command = this.command with {AggregateId = -1};
        return this;
    }

    public Task DoSomething()
    {
        return CallCommand(this.command, ConfigureServices);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<SampleAggregateRepository>(_ => new SampleAggregateRepositoryMock());
    }
}