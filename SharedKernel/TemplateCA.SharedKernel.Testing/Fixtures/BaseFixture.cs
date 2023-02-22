using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TemplateCA.SharedKernel.Domain;
using TemplateCA.SharedKernel.Testing.Extensions;
using TemplateCA.SharedKernel.Testing.Mocks;

namespace TemplateCA.SharedKernel.Testing.Fixtures;

public abstract class BaseFixture : IDisposable
{
    public Exception? Exception { get; protected set; }
    public abstract void Dispose();

    protected ServiceProvider BuildServicesFor(Action<IServiceCollection> configure, params Assembly[] assemblies)
    {
        Assembly[] assembliesToLoad = assemblies.SelectMany(GetAllContextAssemblies).ToArray();
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection = serviceCollection
            .AddDispatcher(assembliesToLoad)
            .AddScoped(typeof(ILogger<>), typeof(LoggerMock<>))
            .AddScoped(typeof(ILogger), typeof(LoggerMock<Object>))
            .AddScoped<DateTimeProvider, SystemDateTimeProvider>()
            .AddScoped<MediatrDomainEventPublisher>()
            .AddScoped(s => new DomainEventPublisherSpy(s.GetService<MediatrDomainEventPublisher>()!))
            .AddScoped<DomainEventPublisher, DomainEventPublisherMock>();
        //     .AddCommonImplementation()
        //     .AddScoped(typeof(ILogger<>), typeof(LoggerMock<>))
        //     .AddSingleton<IMapper>(_ => new Mapper(Mapping.GetConfiguration(assembliesToLoad)))
        //     .AddScoped<UnitOfWork>(_ => Substitute.For<UnitOfWork>())
        //     .AddScoped<MediatrIntegrationPublisher>()
        //     .AddScoped(s => new IntegrationEventPublisherSpy(s.GetService<MediatrIntegrationPublisher>()!))
        //     .AddScoped<IntegrationEventPublisher, IntegrationEventPublisherMock>();
        configure(serviceCollection);
        return serviceCollection
            .BuildServiceProvider();
    }

    private IEnumerable<Assembly> GetAllContextAssemblies(Assembly assembly)
    {
        String assemblyFullName = assembly.FullName!;
        if (assemblyFullName.Contains("Applications"))
        {
            yield return Assembly.Load(assemblyFullName.Replace("Applications", "Domain"));
            yield return Assembly.Load(assemblyFullName.Replace("Applications", "Adapters"));
        }
        else if (assemblyFullName.Contains("Domain"))
        {
            yield return Assembly.Load(assemblyFullName.Replace("Domain", "Applications"));
            yield return Assembly.Load(assemblyFullName.Replace("Domain", "Adapters"));
        }
        else if (assemblyFullName.Contains("Adapters"))
        {
            yield return Assembly.Load(assemblyFullName.Replace("Adapters", "Applications"));
            yield return Assembly.Load(assemblyFullName.Replace("Adapters", "Domain"));
        }

        yield return assembly;
    }
}