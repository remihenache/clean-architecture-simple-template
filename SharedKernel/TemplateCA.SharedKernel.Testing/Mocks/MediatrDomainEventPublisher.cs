using MediatR;
using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Testing.Mocks;

public class MediatrDomainEventPublisher : DomainEventPublisher
{
    private readonly IMediator mediator;

    public MediatrDomainEventPublisher(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Publish(params DomainEvent[] events)
    {
        foreach (DomainEvent domainEvent in events) await this.mediator.Publish(domainEvent);
    }

    public async Task Publish(EventAggregate eventAggregate)
    {
        await Publish(eventAggregate.Events.ToArray());
    }
}