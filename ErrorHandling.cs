﻿using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Common.Exceptions;
using SpellSmarty.Application.Common.Response;

namespace SpellSmarty.Api
{
    public class ErrorHandling<TResponse> : IActionResult
    {
        private readonly BaseResponse<TResponse> _exception;

        public ErrorHandling(BaseResponse<TResponse> e)
        {
            _exception = e;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(new
            {
                Value = _exception.Message,
            });
            objectResult.StatusCode = 500;

            if (_exception.Exception is BadRequestException badreq)
            {
                objectResult.StatusCode = 400;
                objectResult.Value = _exception.Message;
            }
            if (_exception.Exception is ForbiddenAccessException forbid)
            {
                objectResult.StatusCode = 403;
                objectResult.Value = _exception.Message;
            }
            if (_exception.Exception is NotFoundException notfound)
            {
                objectResult.StatusCode = 404;
                objectResult.Value = _exception.Message;
            }
            if (_exception.Exception is ValidationException validate)
            {
                objectResult.StatusCode = 400;
                objectResult.Value = _exception.Message;
            }
            if (_exception.Message == null) objectResult.Value = _exception.Exception.Message;
            await objectResult.ExecuteResultAsync(context);
        }
    }
}