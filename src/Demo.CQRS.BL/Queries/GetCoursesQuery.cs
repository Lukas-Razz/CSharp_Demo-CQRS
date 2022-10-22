using Demo.CQRS.Domain;
using MediatR;
using Optional;

namespace Demo.CQRS.BL.Queries
{
    // IRequest is MediatR way of representing query message
    // This one is a query that returns a collection of Courses
    public class GetCoursesQuery : IRequest<IEnumerable<Course>>
    {
        // Option is (one of) implementation of Maybe monad in C#
        // It is safe way of expressing null object, without actually using nulls
        private Option<DateTime> Before { get; init; }

        private Option<DateTime> After { get; init; }
        private Option<string> AtLocation { get; init; }
    }
}