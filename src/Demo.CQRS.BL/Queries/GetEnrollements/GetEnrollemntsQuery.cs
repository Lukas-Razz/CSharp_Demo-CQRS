using Demo.CQRS.Domain;
using MediatR;

namespace Demo.CQRS.BL.Queries.GetEnrollments
{
    public class GetEnrollemntsQuery : IRequest<IEnumerable<Enrollment>>
    {
        public Guid UserId { get; init; }
    }
}