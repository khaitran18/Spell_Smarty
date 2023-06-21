using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Common.Exceptions;

namespace SpellSmarty.Api
{
    public class ErrorHandling : IActionResult
    {
        private readonly Exception _exception;

        public ErrorHandling(Exception e)
        {
            _exception = e;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(new
            {
                Message = "An error occurred.",
                StatusCode = 500
            });
            if (_exception is BadRequestException badreq)
            {
                objectResult.StatusCode = 400;
                objectResult.Value = _exception.Message;
            }
            if (_exception is ForbiddenAccessException forbid)
            {
                objectResult.StatusCode = 403;
                objectResult.Value = _exception.Message;
            }
            if (_exception is NotFoundException notfound)
            {
                objectResult.StatusCode = 404;
                objectResult.Value = _exception.Message;
            }
            if (_exception is ValidationException valid)
            {
                objectResult.StatusCode = 400;
                objectResult.Value = _exception.Message;
            }
            await objectResult.ExecuteResultAsync(context);
        }
    }
}