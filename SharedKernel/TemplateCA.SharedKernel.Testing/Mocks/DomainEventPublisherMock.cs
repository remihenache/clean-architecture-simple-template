using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Testing.Mocks;

public class DomainEventPublisherMock : DomainEventPublisher
{
    private readonly List<DomainEvent> publishedEvents = new();

    public IReadOnlyCollection<DomainEvent> PublishedEvents => this.publishedEvents;

    public Task Publish(params DomainEvent[] events)
    {
        this.publishedEvents.AddRange(events);
        return Task.CompletedTask;
    }

    public Task Publish(EventAggregate eventAggregate)
    {
        this.publishedEvents.AddRange(eventAggregate.Events);
        return Task.CompletedTask;
    }
}