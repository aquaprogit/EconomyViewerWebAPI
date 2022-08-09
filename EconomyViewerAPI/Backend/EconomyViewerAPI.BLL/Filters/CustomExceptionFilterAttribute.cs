
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using EconomyViewerAPI.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EconomyViewerAPI.BLL.Filters;
public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private IWebHostEnvironment _hostEnvironment;

    public CustomExceptionFilterAttribute(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public override void OnException(ExceptionContext context)
    {
        Exception? ex = context.Exception;
        string message = ex.Message;
        IActionResult actionResult = ex switch {
            ItemNotFoundException => new NotFoundObjectResult(ex.Message) { StatusCode = 404 },
            _ => new BadRequestObjectResult(ex.Message) { StatusCode = 400 },
        };
        context.ExceptionHandled = true;
        context.Result = actionResult;
    }
}
