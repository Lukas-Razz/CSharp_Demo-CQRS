using MediatR;

namespace Demo.CQRS.BL.Commands.EnrollToCourse
{
    // IRequestHandler is MediatR way of representing handler of a command message
    // This one can handle EnrollToCourseCommand and returns nothing
    public class EnrollToCourseCommandHandler : IRequestHandler<EnrollToCourseCommand>
    {
        public Task<Unit> Handle(EnrollToCourseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}