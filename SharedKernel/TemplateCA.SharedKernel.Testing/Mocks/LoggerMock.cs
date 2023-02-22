using Microsoft.Extensions.Logging;

namespace TemplateCA.SharedKernel.Testing.Mocks;

public class LoggerMock<T> : ILogger<T>, IDisposable
{
    public void Dispose()
    {
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, String> formatter)
    {
    }

    public Boolean IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        return new LoggerMock<TState>();
    }
}