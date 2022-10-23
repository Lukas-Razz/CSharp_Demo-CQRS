using Demo.CQRS.Domain;

namespace Demo.CQRS.BL.Contracts
{
    public interface IEnrollmentRepository
    {
        Task<Guid> CreateAsync(Enrollment enrollment);
    }
}
