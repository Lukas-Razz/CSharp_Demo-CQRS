using Demo.CQRS.Domain;
using MediatR;

namespace Demo.CQRS.BL.Queries
{
    // IRequestHandler is MediatR way of representing handler of a query message
    // This one can handle GetCoursesQuery and returns a collection of Courses
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<Course>>
    {
        public Task<IEnumerable<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}