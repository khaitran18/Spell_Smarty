﻿using FluentValidation;
using MediatR;
using SpellSmarty.Application.Common.Exceptions;
namespace SpellSmarty.Application.Common.Behaviour
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationFailures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .ToList();

            if (validationFailures.Any())
            {
                var error = string.Join("\r\n", validationFailures);
                Application.Common.Exceptions.ValidationException exception = new Application.Common.Exceptions.ValidationException();
                return (TResponse)Activator.CreateInstance(typeof(TResponse),true, error,exception);
            }
            return await next();
        }
    }
}
