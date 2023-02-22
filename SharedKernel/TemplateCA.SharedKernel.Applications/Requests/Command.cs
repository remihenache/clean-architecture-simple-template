using MediatR;
using TemplateCA.SharedKernel.Domain;

namespace TemplateCA.SharedKernel.Applications.Requests;

public interface Command : IRequest<CommandResult>
{
}

public class CommandResult
{
    private CommandResult(Aggregate aggregate)
    {
        Aggregate = aggregate;
    }

    private CommandResult(Exception exception)
    {
        Exception = exception;
    }

    public Aggregate? Aggregate { get; }

    public Exception? Exception { get; }

    public Boolean IsValid()
    {
        return Exception == null;
    }

    public static CommandResult FromAggregate(Aggregate aggregate)
    {
        return new CommandResult(aggregate);
    }

    public static CommandResult FromException(Exception exception)
    {
        return new CommandResult(exception);
    }
}