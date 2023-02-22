using MediatR;

namespace TemplateCA.SharedKernel.Applications.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<Authorizer<TRequest>> authorizers;

    public AuthorizationBehavior(IEnumerable<Authorizer<TRequest>> authorizers)
    {
        this.authorizers = authorizers;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        foreach (Authorizer<TRequest> authorizer in this.authorizers)
            await authorizer.AuthorizeAsync(request, cancellationToken);

        return await next();
    }
}