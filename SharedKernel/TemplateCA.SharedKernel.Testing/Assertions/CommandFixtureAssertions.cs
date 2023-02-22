using FluentAssertions.Primitives;
using TemplateCA.SharedKernel.Testing.Fixtures;

namespace TemplateCA.SharedKernel.Testing.Assertions;

public abstract class CommandFixtureAssertions<TFixture, TAssertions> :
    BaseFixtureAssertions<TFixture, TAssertions>
    where TFixture : CommandFixture
    where TAssertions : ReferenceTypeAssertions<TFixture, TAssertions>
{
    public CommandFixtureAssertions(TFixture instance)
        : base(instance)
    {
    }

    protected override String Identifier => "CommandFixtureAssertions";
}