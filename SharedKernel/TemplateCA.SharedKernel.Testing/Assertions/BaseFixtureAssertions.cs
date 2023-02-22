using FluentAssertions;
using FluentAssertions.Primitives;
using FluentValidation;
using TemplateCA.SharedKernel.Domain.Exceptions;
using TemplateCA.SharedKernel.Testing.Fixtures;

namespace TemplateCA.SharedKernel.Testing.Assertions;

public abstract class BaseFixtureAssertions<TFixture, TAssertions> :
    ReferenceTypeAssertions<TFixture, TAssertions>
    where TFixture : BaseFixture
    where TAssertions : ReferenceTypeAssertions<TFixture, TAssertions>
{
    public BaseFixtureAssertions(TFixture instance)
        : base(instance)
    {
    }

    protected override String Identifier => "BaseFixtureAssertions";


    public AndConstraint<TAssertions> HaveThrowSecurityException()
    {
        Subject.Exception.Should().NotBeNull();
        Subject.Exception.Should().BeAssignableTo<SecurityException>();

        return new AndConstraint<TAssertions>((this as TAssertions)!);
    }

    public AndConstraint<TAssertions> HaveThrowValidationException()
    {
        Subject.Exception.Should().NotBeNull();

        if (Subject.Exception!.GetType() == typeof(ValidationException))
            Subject.Exception.Should().BeAssignableTo<ValidationException>();
        else
            Subject.Exception.Should().BeAssignableTo<DomainValidationException>();
        return new AndConstraint<TAssertions>((this as TAssertions)!);
    }
}