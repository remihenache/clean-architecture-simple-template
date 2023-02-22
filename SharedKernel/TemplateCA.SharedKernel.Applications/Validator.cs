using FluentValidation;
using JetBrains.Annotations;

namespace TemplateCA.SharedKernel.Applications;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public class Validator<T> : AbstractValidator<T>
{
}