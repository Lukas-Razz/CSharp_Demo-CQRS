using FluentValidation;
using MediatR;

namespace Demo.CQRS.BL.Behavior
{
    // This utilises the fact that MediatR is handling the Commands and Queries
    // It is basically a Decorator pattern
    // The request in MediatR gets intercepted and is processed if it fits the type.
    // After processing, the request is passed to further processing (another behavior or handler). See the "return next()" at the end.
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request,
                                      RequestHandlerDelegate<TResponse> next,
                                      CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return next();
        }
    }
}
