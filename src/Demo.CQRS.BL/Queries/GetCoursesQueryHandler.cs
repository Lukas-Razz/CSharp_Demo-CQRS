using Demo.CQRS.BL.Contracts;
using Demo.CQRS.Domain;
using MediatR;

namespace Demo.CQRS.BL.Queries
{
    // IRequestHandler is MediatR way of representing handler of a query message
    // This one can handle GetCoursesQuery and returns a collection of Courses
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<Course>>
    {
        private ICourseRepository _courseRepository;
        public GetCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var predicates = new List<Func<Course, bool>>();
            request.AtLocation.MatchSome(location => predicates.Add((Course course) => location == course.Location));
            request.Before.MatchSome(before => predicates.Add((Course course) => before > course.Start));
            request.After.MatchSome(after => predicates.Add((Course course) => after < course.Start));

            var combinedPredicate = predicates.AsEnumerable().Aggregate((acc, x) => CombinePredicates(acc, x));

            var results = await _courseRepository.GetAll();

            return results.Where(combinedPredicate);
        }

        private Func<Course, bool> CombinePredicates<Course>(Func<Course, bool> predicate1, Func<Course, bool> predicate2)
        {
            return course => predicate1(course) && predicate2(course);
        }
    }
}