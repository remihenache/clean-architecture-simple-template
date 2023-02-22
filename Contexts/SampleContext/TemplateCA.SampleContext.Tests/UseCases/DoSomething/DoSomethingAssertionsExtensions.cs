namespace TemplateCA.SampleContext.Tests.UseCases.DoSomething;

public static class DoSomethingAssertionsExtensions
{
    public static DoSomethingAssertions Should(this DoSomethingFixture instance)
    {
        return new DoSomethingAssertions(instance);
    }
}