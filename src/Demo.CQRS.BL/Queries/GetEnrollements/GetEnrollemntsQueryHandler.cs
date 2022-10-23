using Demo.CQRS.BL.Contracts;
using Demo.CQRS.Domain;
using MediatR;

namespace Demo.CQRS.BL.Queries.GetEnrollments
{
    // IRequestHandler is MediatR way of representing handler of a query message
    // This one can handle GetCoursesQuery and returns a collection of Courses
    public class GetEnrollemntsQueryHandler : IRequestHandler<GetEnrollemntsQuery, IEnumerable<Enrollment>>
    {
        private IEnrollmentRepository _enrollmentRepository;
        public GetEnrollemntsQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<Enrollment>> Handle(GetEnrollemntsQuery request, CancellationToken cancellationToken)
        {
            var results = await _enrollmentRepository.GetAllAsync();

            return results.Where(e => e.UserId == request.UserId && !e.CanceledTimestamp.HasValue);
        }
    }
}