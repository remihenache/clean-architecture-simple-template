using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TemplateCA.SharedKernel.Applications;
using TemplateCA.SharedKernel.Applications.Presenters;
using TemplateCA.SharedKernel.Applications.Requests;

namespace TemplateCA.SharedKernel.Testing.Fixtures;

public abstract class QueryFixture : BaseFixture
{
}

public abstract class QueryFixture<T> : QueryFixture, IDisposable
{
    private ServiceProvider? services;
    public T? Result { get; private set; }

    public override void Dispose()
    {
        //Nothing to dispose
    }

    protected async Task CallQuery(
        Query<T> query,
        Action<IServiceCollection> configure,
        params Assembly[] additionalAssemblies)
    {
        Assembly[] assemblies = additionalAssemblies.Append(query.GetType().Assembly).ToArray();

        this.services = BuildServicesFor(configure, assemblies);
        Dispatcher dispatcher = this.services.GetService<Dispatcher>() ?? throw new InvalidProgramException();
        try
        {
            Result = await dispatcher.Send(query).AsItSelf();
        }
        catch (Exception e)
        {
            Exception = e;
        }
    }
}