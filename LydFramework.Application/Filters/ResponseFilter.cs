using LydFramework.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LydFramework.Application.Filters
{
    public class ResponseFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var r = await next();
            if (r.Result != null)
            {
                //响应是一个对象结果
                if (r.Result is ObjectResult)
                {
                    ObjectResult? objectResult = r.Result as ObjectResult;
                    if (objectResult?.Value?.GetType().Name == nameof(ResultDto))
                    {
                        var result = objectResult.Value as ResultDto;
                        r.Result = new ObjectResult(result);
                    }
                    else
                    {
                        r.Result = new ObjectResult(new ResultDto(200, null, objectResult?.Value));
                    }
                }

                //响应是空结果
                else if (r.Result is EmptyResult)
                    r.Result = new ObjectResult(new ResultDto(204,"Success"));

                //响应结果是ResultDto
                else if (r.Result is ResultDto resultDto)
                    r.Result = new ObjectResult(resultDto);
            }
            else
            {
                r.Result = new ObjectResult(new ResultDto(204,null));
            }
        }
    }
}
