using Demo.CQRS.BL.Contracts;
using Demo.CQRS.Domain;
using MediatR;

namespace Demo.CQRS.BL.Commands.EnrollToCourse
{
    // IRequestHandler is MediatR way of representing handler of a command message
    // This one can handle EnrollToCourseCommand and returns nothing
    public class EnrollToCourseCommandHandler : IRequestHandler<EnrollToCourseCommand>
    {
        private IEnrollmentRepository _enrollmentRepository;
        private IEmailService _emailService;

        public EnrollToCourseCommandHandler(IEnrollmentRepository enrollmentRepository, IEmailService emailService)
        {
            _enrollmentRepository = enrollmentRepository;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(EnrollToCourseCommand request, CancellationToken cancellationToken)
        {
            var enrollment = new Enrollment
            {
                ContactEmail = request.ContactEmail,
                UserId = request.UserId,
                Course = request.Course,
            };
            await _enrollmentRepository.CreateAsync(enrollment);

            var email = new Email
            (
                Receiver: request.ContactEmail,
                Subject: "Course enrolled",
                Body: @$"Dear customer,
                You are successfully enroled to the course {request.Course.Name}.
                Best regards,
                DemoCourse Team"
            );
            await _emailService.SendEmailAsync(email);

            return Unit.Value;
        }
    }
}