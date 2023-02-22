using MediatR;
using TemplateCA.SharedKernel.Applications.Requests;
using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Applications.Behaviors;

public class DomainEventDispatcherBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Command, IRequest<TResponse>
    where TResponse : CommandResult
{
    private readonly DomainEventPublisher publisher;

    public DomainEventDispatcherBehavior(DomainEventPublisher publisher)
    {
        this.publisher = publisher;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response = await next();
        if (response.Aggregate is not EventAggregate eventAggregate)
            return response;

        await this.publisher.Publish(eventAggregate);
        eventAggregate.ClearEvents();
        return response;
    }
}