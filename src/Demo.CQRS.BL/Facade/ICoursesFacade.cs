using Demo.CQRS.Domain;

namespace Demo.CQRS.BL.Facade
{
    public interface ICoursesFacade
    {
        Task<IEnumerable<Course>> GetOpenCoursesIn(string location);
        Task Enroll(Course course, Guid userId, string email);
        Task CancelEnroll(Enrollment enrollment);
        Task<IEnumerable<Enrollment>> GetPendingEnrollements(Guid userId);
    }
}