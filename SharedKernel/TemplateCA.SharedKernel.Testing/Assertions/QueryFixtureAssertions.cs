using FluentAssertions.Primitives;
using TemplateCA.SharedKernel.Testing.Fixtures;

namespace TemplateCA.SharedKernel.Testing.Assertions;

public abstract class QueryFixtureAssertions<TFixture, TAssertions> :
    BaseFixtureAssertions<TFixture, TAssertions>
    where TFixture : QueryFixture
    where TAssertions : ReferenceTypeAssertions<TFixture, TAssertions>
{
    public QueryFixtureAssertions(TFixture instance)
        : base(instance)
    {
    }

    protected override String Identifier => "QueryFixtureAssertions";
}