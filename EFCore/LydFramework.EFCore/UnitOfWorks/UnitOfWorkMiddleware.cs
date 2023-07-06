using EntityFrameworkCore.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;


namespace LydFramework.EFCore.UnitOfWorks
{
    public class UnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        public UnitOfWorkMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var metadata = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;
            var unit = metadata.GetMetadata<EFCoreUnitOfWorkAttribute>();

            #region 不需要工作单元
            if (unit == null)
            {
                await Console.Out.WriteLineAsync("不需要EFCore工作单元");
                await _next.Invoke(context);
            }
            #endregion

            #region 需要工作单元
            else
            {
                await Console.Out.WriteLineAsync("需要EFCore工作单元");
                //获取服务中多个DbContext
                var unitOfWorks = context.RequestServices.GetServices<IUnitOfWork>();
                foreach (var unitOfWork in unitOfWorks)
                {
                    // 开启事务
                    await unitOfWork.BeginTransactionAsync();
                }
                try
                {
                    await _next.Invoke(context);

                    foreach (var unitOfWork in unitOfWorks)
                    {
                        // 提交事务
                        await unitOfWork.CommitTransactionAsync();
                    }
                    await Console.Out.WriteLineAsync("提交EFCore工作单元");
                }
                catch (Exception ex)
                {
                    foreach (var unitOfWork in unitOfWorks)
                    {
                        await unitOfWork.RollbackTransactionAsync();
                    }
                    await Console.Out.WriteLineAsync("回滚EFCore工作单元");
                    throw ex;
                }
            }
            #endregion
        }
    }
}
