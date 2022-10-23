using Demo.CQRS.Domain;

namespace Demo.CQRS.BL.Contracts
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
    }
}
