using Demo.CQRS.Domain;
using MediatR;

namespace Demo.CQRS.BL.Commands.EnrollToCourse
{
    // IRequest is MediatR way of representing command message
    // This one is a command that returns nothing
    public class EnrollToCourseCommand : IRequest
    {
        public Course Course { get; set; }
        public Guid UserId { get; set; }
        public string ContactEmail { get; set; }
    }
}