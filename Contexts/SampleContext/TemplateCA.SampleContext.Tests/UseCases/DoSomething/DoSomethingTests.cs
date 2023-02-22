namespace TemplateCA.SampleContext.Tests.UseCases.DoSomething;

public class DoSomethingTests
{
    private readonly DoSomethingFixture fixture;

    public DoSomethingTests()
    {
        this.fixture = new DoSomethingFixture();
    }

    [Fact]
    public async Task ShouldMadeSomethingDone()
    {
        await this.fixture.DoSomething();
        this.fixture.Should().SomethingWasDone();
    }

    [Fact]
    public async Task CannotDoSomethingIfIdIsNullOrEmpty()
    {
        await this.fixture.TryToAccessNegativeId().DoSomething();
        this.fixture.Should().HaveThrowValidationException();
    }
}