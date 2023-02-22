using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TemplateCA.SharedKernel.Applications;
using TemplateCA.SharedKernel.Applications.Behaviors;
using TemplateCA.SharedKernel.Applications.Requests;
using TemplateCA.SharedKernel.Extensions;
using TemplateCA.SharedKernel.Testing.Mocks;

namespace TemplateCA.SharedKernel.Testing.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddDispatcher(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services = services
            .AddScoped<Dispatcher, MediatorDispatcher>()
            .AddScoped(typeof(PresenterQueryHandler<,>), typeof(PresenterQueryHandler<,>))
            .AddMediatR(assemblies)
            .AddScoped<IMediatorServiceTypeConverter, GenericHandlerConverter>()
            .AddScoped(sp =>
                MediatorServiceFactory.Wrap(sp.GetService!, sp.GetServices<IMediatorServiceTypeConverter>()))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(DomainEventDispatcherBehavior<,>));
        foreach (Assembly assembly in assemblies)
            services = services.RegisterRequestHandlers(assembly)
                .AddAuthorizersFromAssembly(assembly)
                .AddValidatorsFromAssembly(assembly)
                .AddQueryHandlers(assembly);

        return services;
    }

    private static IServiceCollection RegisterRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        Type handlerType = typeof(IRequestHandler<,>);
        assembly.GetTypesAssignableTo(handlerType).ForEach(type =>
        {
            services = services
                .AddScoped(type);
        });
        return services;
    }

    private static IServiceCollection AddQueryHandlers(
        this IServiceCollection services,
        Assembly assembly)
    {
        Type authorizerType = typeof(QueryHandler<,>);
        assembly.GetTypesAssignableTo(authorizerType).Where(x => !x.IsGenericType).ToList().ForEach(type =>
        {
            services = RegisterService(services, type);
        });

        return services;
    }

    private static IServiceCollection AddAuthorizersFromAssembly(
        this IServiceCollection services,
        Assembly assembly)
    {
        Type authorizerType = typeof(Authorizer<>);
        assembly.GetTypesAssignableTo(authorizerType).Where(x => !x.IsGenericType).ToList().ForEach(type =>
        {
            services = RegisterService(services, type);
        });

        return services;
    }

    private static IServiceCollection RegisterService(
        IServiceCollection services,
        TypeInfo type)
    {
        foreach (Type implementedInterface in type.ImplementedInterfaces)
            services = services
                .AddScoped(implementedInterface, type);
        return services;
    }
}

internal static class MediatorServiceFactory
{
    public static ServiceFactory Wrap(
        ServiceFactory serviceFactory,
        IEnumerable<IMediatorServiceTypeConverter> converters)
    {
        return serviceType =>
        {
            Type NoConversion()
            {
                return serviceType;
            }

            ConverterDelegate convertServiceType = converters
                .Reverse()
                .Aggregate((ConverterDelegate) NoConversion, (next, c) => () => c.Convert(serviceType, next));
            return serviceFactory(convertServiceType());
        };
    }
}

internal delegate Type ConverterDelegate();

internal interface IMediatorServiceTypeConverter
{
    Type Convert(Type sourceType, ConverterDelegate next);
}

internal class GenericHandlerConverter : IMediatorServiceTypeConverter
{
    public Type Convert(Type sourceType, ConverterDelegate next)
    {
        Boolean isRequestHandler = sourceType.IsGenericType &&
                                   sourceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
        if (!isRequestHandler) return next();

        Type? requestType =
            sourceType.GenericTypeArguments.FirstOrDefault(t => t.IsAssignableTo(typeof(IBaseRequest)));
        if (requestType == null)
            return next();
        Type? returnType = requestType.GetInterfaces()
            .FirstOrDefault(type =>
                type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableTo(typeof(Query<>)))
            ?.GenericTypeArguments
            .FirstOrDefault();
        if (returnType == null) return next();

        Type handlerType = typeof(PresenterQueryHandler<,>);
        return handlerType.MakeGenericType(requestType, returnType);
    }
}