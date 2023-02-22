using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateCA.SharedKernel.Applications.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IServiceProvider serviceProvider)
    {
        if (ShouldGetValidatorFromNonGenericBaseType())
        {
            Type typeToFind = typeof(IValidator<>).MakeGenericType(typeof(TRequest).BaseType!);
            this.validators = (IEnumerable<IValidator<TRequest>>) serviceProvider.GetServices(typeToFind);
        }
        else
        {
            this.validators = serviceProvider.GetServices<IValidator<TRequest>>();
        }
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!this.validators.Any()) return await next();
        ValidationContext<TRequest> context = new(request);
        List<ValidationFailure> errorsDictionary = this.validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                    ValidationFailure(propertyName, String.Join(". ", errorMessages.Distinct().ToArray()))
            )
            .ToList();
        if (errorsDictionary.Any())
            throw new ValidationException(errorsDictionary);
        return await next();
    }

    private static Boolean ShouldGetValidatorFromNonGenericBaseType()
    {
        Type requestType = typeof(TRequest);
        return requestType.IsGenericType
               //&& requestType.GenericTypeArguments.First().GenericTypeArguments.Any()
               && requestType.BaseType != null
               && requestType.BaseType.IsAssignableTo(typeof(IBaseRequest));
    }
}