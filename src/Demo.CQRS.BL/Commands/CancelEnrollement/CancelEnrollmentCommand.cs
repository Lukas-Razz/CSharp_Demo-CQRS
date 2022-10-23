using Demo.CQRS.Domain;
using MediatR;

namespace Demo.CQRS.BL.Commands.CancelEnrollment
{
    public class CancelEnrollmentCommand : IRequest
    {
        public Enrollment Enrollment { get; set; }
    }
}