using TemplateCA.SharedKernel.Applications;

namespace TemplateCA.SampleContext.Applications.DoSomething;

public class DoSomethingAuthorizer : Authorizer<DoSomethingCommand>
{
    public Task AuthorizeAsync(DoSomethingCommand instance, CancellationToken cancellation = default)
    {
        return Task.CompletedTask;
    }
}