namespace TemplateCA.SharedKernel.Domain;

public interface DomainEventPublisher
{
    Task Publish(params DomainEvent[] events);
    Task Publish(EventAggregate eventAggregate);
}