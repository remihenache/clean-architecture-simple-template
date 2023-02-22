// using MediatR;
// using QuarksUp.SharedKernel.Applications;
// using QuarksUp.SharedKernel.Integration;
//
// namespace QuarksUp.Api.Middlewares;
//
// public class IntegrationEventDispatcherBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//     where TRequest : Command, IRequest<TResponse>
//     where TResponse : CommandResult
// {
//     private readonly IntegrationEventPublisher publisher;
//
//     public IntegrationEventDispatcherBehavior(IntegrationEventPublisher publisher)
//     {
//         this.publisher = publisher;
//     }
//
//     public async Task<TResponse> Handle(
//         TRequest request,
//         CancellationToken cancellationToken,
//         RequestHandlerDelegate<TResponse> next)
//     {
//         TResponse response = await next();
//         await this.publisher.Publish();
//
//         return response;
//     }
// }

