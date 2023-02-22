using MediatR;
using Microsoft.Extensions.Logging;
using TemplateCA.SharedKernel.Applications.Requests;
using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Applications.Behaviors;

public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, CommandResult>
    where TRequest : IRequest<TResponse>
    where TResponse : CommandResult
{
    private readonly DateTimeProvider dateTimeProvider;
    private readonly ILogger logger;

    public ExceptionBehavior(ILogger logger, DateTimeProvider dateTimeProvider)
    {
        this.logger = logger;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task<CommandResult> Handle(TRequest request, RequestHandlerDelegate<CommandResult> next,
        CancellationToken cancellationToken)
    {
        IDisposable? dispoable = this.logger.BeginScope(Guid.NewGuid());
        try
        {
            CommandResult result;
            this.logger.LogInformation(
                $"Handler for {typeof(TRequest).FullName} started at {this.dateTimeProvider.GetNowUtc()}");
            result = await next();
            this.logger.LogInformation(
                $"Handler for {typeof(TRequest).FullName} end at {this.dateTimeProvider.GetNowUtc()}");
            return result;
        }
        catch (Exception ex)
        {
            this.logger.LogInformation(
                $"Handler for {typeof(TRequest).FullName} raise exception at {this.dateTimeProvider.GetNowUtc()}");
            this.logger.LogError(ex, "Unhandle exception has been raised");
            return CommandResult.FromException(ex);
        }
        finally
        {
            dispoable?.Dispose();
        }
    }
}