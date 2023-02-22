using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TemplateCA.SharedKernel.Applications;
using TemplateCA.SharedKernel.Applications.Requests;
using TemplateCA.SharedKernel.Domain;
using TemplateCA.SharedKernel.Testing.Mocks;

namespace TemplateCA.SharedKernel.Testing.Fixtures;

public abstract class CommandFixture : BaseFixture, IDisposable
{
    public readonly List<DomainEvent> PublishedDomainEvents = new();
    private ServiceProvider? services;
    public CommandResult? Result { get; private set; }

    public override void Dispose()
    {
        //Nothing to dispose
    }

    protected async Task CallCommand<T>(T command, Action<IServiceCollection> configure)
        where T : Command
    {
        this.services = BuildServicesFor(configure, command.GetType().Assembly);
        using IServiceScope serviceScope = this.services.CreateScope();
        Dispatcher dispatcher = serviceScope.ServiceProvider.GetService<Dispatcher>() ??
                                throw new InvalidProgramException();
        try
        {
            Result = await dispatcher.Send(command);
            Exception = Result.Exception;
        }
        catch (Exception e)
        {
            Exception = e;
        }

        DomainEventPublisherMock publisher =
            (serviceScope.ServiceProvider.GetService<DomainEventPublisher>() as DomainEventPublisherMock)!;
        this.PublishedDomainEvents.AddRange(publisher.PublishedEvents);
    }


    protected async Task CallEvent(
        DomainEvent @event,
        Action<IServiceCollection> configureServices,
        params Assembly[] assemblies)
    {
        this.services = BuildServicesFor(configureServices,
            assemblies.Concat(new[] {@event.GetType().Assembly}).Distinct().ToArray());
        using IServiceScope serviceScope = this.services.CreateScope();
        DomainEventPublisherSpy publisher = serviceScope.ServiceProvider.GetService<DomainEventPublisherSpy>()!;
        try
        {
            await publisher.Publish(@event);
        }
        catch (Exception e)
        {
            Exception = e;
        }

        DomainEventPublisherMock publisherMocked =
            (serviceScope.ServiceProvider.GetService<DomainEventPublisher>() as DomainEventPublisherMock)!;
        this.PublishedDomainEvents.AddRange(publisher.PublishedEvents);
        this.PublishedDomainEvents.AddRange(publisherMocked.PublishedEvents);
    }
}