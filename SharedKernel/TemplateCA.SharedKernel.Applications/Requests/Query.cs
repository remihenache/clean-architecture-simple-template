using MediatR;
using TemplateCA.SharedKernel.Applications.Presenters;

namespace TemplateCA.SharedKernel.Applications.Requests;

public interface Query : IRequest
{
}

public interface Query<TOutput> : IRequest<Presentable<TOutput>>
{
}