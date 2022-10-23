using Demo.CQRS.BL.Contracts;
using Demo.CQRS.DAL.Enrollment;
using Demo.CQRS.Domain;

namespace Demo.CQRS.Infrastructure
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private Func<EnrollmentContext> _contextFactory;
        public EnrollmentRepository(Func<EnrollmentContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Guid> CreateAsync(Enrollment enrollment)
        {
            using var context = _contextFactory();

            var enrollmentEntity = new CourseEnrollment
            { 
                UserId = enrollment.UserId,
                ContactEmail = enrollment.ContactEmail,
                CourseId = enrollment.Course.Id,
                EnrollmentTimestamp = DateTime.UtcNow,
            };

            var entry = await context.CourseEnrollments.AddAsync(enrollmentEntity);

            await context.SaveChangesAsync();

            return entry.Entity.Id;
        }
    }
}
