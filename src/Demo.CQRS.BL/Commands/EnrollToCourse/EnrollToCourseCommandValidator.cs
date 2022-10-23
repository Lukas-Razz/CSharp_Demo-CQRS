using FluentValidation;

namespace Demo.CQRS.BL.Commands.EnrollToCourse
{
    // This encapsulates validation logic
    // It takes the various validation rules outside the object definition itself
    public class EnrollToCourseCommandValidator : AbstractValidator<EnrollToCourseCommand>
    {
        // There ale lots of helpful methods for creating validation rules
        // The messages are just overrides, you get them by default with the rules
        // Notice the pattern of the message. See the FluentValidation documentation how it works.
        public EnrollToCourseCommandValidator()
        {
            RuleFor(c => c.Course)
                .NotEmpty()
                    .WithMessage("{PropertyName} needs to be specified.");

            RuleFor(c => c.UserId)
                .NotEmpty()
                    .WithMessage("{PropertyName} needs to be specified.");

            RuleFor(c => c.ContactEmail)
                .NotEmpty()
                    .WithMessage("{PropertyName} needs to be specified.")
                .EmailAddress()
                    .WithMessage("{PropertyValue} is not a valid email address.");
        }
    }
}