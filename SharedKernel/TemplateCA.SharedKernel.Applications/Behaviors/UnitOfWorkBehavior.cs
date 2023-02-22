// using MediatR;
// using QuarksUp.SharedKernel.Applications;
// using QuarksUp.SharedKernel.Infrastructure;
//
// namespace QuarksUp.Api.Middlewares;
//
// public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//     where TRequest : IRequest<TResponse>
// {
//     private readonly UnitOfWork unitOfWork;
//
//     public UnitOfWorkBehavior(UnitOfWork unitOfWork)
//     {
//         this.unitOfWork = unitOfWork;
//     }
//
//     public async Task<TResponse> Handle(
//         TRequest request,
//         CancellationToken cancellationToken,
//         RequestHandlerDelegate<TResponse> next)
//     {
//         if (!typeof(TRequest).IsAssignableTo(typeof(Command)))
//             return await next();
//         if (this.unitOfWork.TransactionIsOpened)
//             return await next();
//
//         try
//         {
//             this.unitOfWork.Begin();
//             TResponse response = await next();
//             this.unitOfWork.Commit();
//             return response;
//         }
//         catch (Exception)
//         {
//             this.unitOfWork.Rollback();
//             throw;
//         }
//         finally
//         {
//             this.unitOfWork.Dispose();
//         }
//     }
// }
//
// //
// // public class ExceptionHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
// //     where TRequest : IRequest<TResponse>
// // {
// //     private readonly ILogger logger;
// //
// //     public ExceptionHandlerBehavior(ILogger logger)
// //     {
// //         this.logger = logger;
// //     }
// //
// //     public async Task<TResponse> Handle(
// //         TRequest request,
// //         CancellationToken cancellationToken,
// //         RequestHandlerDelegate<TResponse> next)
// //     {
// //         try
// //         {
// //             await next();
// //         }
// //         catch (Exception e)
// //         {
// //            
// //         }
// //     }
// // }

