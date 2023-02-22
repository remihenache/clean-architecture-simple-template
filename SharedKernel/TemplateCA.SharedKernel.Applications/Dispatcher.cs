using TemplateCA.SharedKernel.Applications.Presenters;
using TemplateCA.SharedKernel.Applications.Requests;

namespace TemplateCA.SharedKernel.Applications;

public interface Dispatcher
{
    /// <summary>
    ///     Asynchronously send a request to a single handler
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    /// <param name="request">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains the handler response</returns>
    Task<Presentable<TResponse>> Send<TResponse>(Query<TResponse> request,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Asynchronously send a request to a single handler
    /// </summary>
    /// <param name="request">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation.</returns>
    Task<CommandResult> Send(Command request, CancellationToken cancellationToken = default);
}