using FluentAssertions;
using TemplateCA.SampleContext.Domain;
using TemplateCA.SharedKernel.Testing.Assertions;

namespace TemplateCA.SampleContext.Tests.UseCases.DoSomething;

public class DoSomethingAssertions : CommandFixtureAssertions<DoSomethingFixture, DoSomethingAssertions>
{
    public DoSomethingAssertions(DoSomethingFixture instance) : base(instance)
    {
    }

    public AndConstraint<DoSomethingAssertions> SomethingWasDone()
    {
        SomethingDone? somethingDone = Subject.PublishedDomainEvents.OfType<SomethingDone>().FirstOrDefault();
        somethingDone.Should().NotBeNull();

        return new AndConstraint<DoSomethingAssertions>(this);
    }
}