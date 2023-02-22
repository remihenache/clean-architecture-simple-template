namespace TemplateCA.SharedKernel.Applications.Presenters;

public interface Presentable<TInput> : Presentable
{
    Task<TInput> AsItSelf();
}

public interface Presentable
{
    Task<TOutput> Present<TOutput>();
}