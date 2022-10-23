using Demo.CQRS.BL.Contracts;
using Optional;
using MediatR;

namespace Demo.CQRS.BL.Commands.CancelEnrollment
{
    public class CancelEnrollmentCommandHandler : IRequestHandler<CancelEnrollmentCommand>
    {
        private IEnrollmentRepository _enrollmentRepository;
        private IEmailService _emailService;

        public CancelEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IEmailService emailService)
        {
            _enrollmentRepository = enrollmentRepository;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(CancelEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var canceledEnrollment = request.Enrollment;
            canceledEnrollment.CanceledTimestamp = DateTime.UtcNow.Some();

            await _enrollmentRepository.UpdateAsync(canceledEnrollment);

            var email = new Email
            (
                Receiver: canceledEnrollment.ContactEmail,
                Subject: "Enrollment canceled",
                Body: @$"Dear customer,
                You have canceled the enrollment to the course {canceledEnrollment.Course.Name}.
                Best regards,
                DemoCourse Team"
            );
            await _emailService.SendEmailAsync(email);

            return Unit.Value;
        }
    }
}