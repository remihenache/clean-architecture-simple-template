using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Testing.Mocks;

public class DomainEventPublisherSpy : DomainEventPublisher
{
    private readonly List<DomainEvent> publishedEvents = new();
    private readonly DomainEventPublisher realPublisher;

    public DomainEventPublisherSpy(DomainEventPublisher realPublisher)
    {
        this.realPublisher = realPublisher;
    }

    public IReadOnlyCollection<DomainEvent> PublishedEvents => this.publishedEvents;

    public Task Publish(params DomainEvent[] events)
    {
        this.publishedEvents.AddRange(events);
        return this.realPublisher.Publish(events);
    }

    public Task Publish(EventAggregate eventAggregate)
    {
        this.publishedEvents.AddRange(eventAggregate.Events);
        return this.realPublisher.Publish(eventAggregate);
    }
}