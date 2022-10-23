using Demo.CQRS.BL.Contracts;
using Demo.CQRS.DAL.Enrollment;
using Demo.CQRS.Domain;
using Microsoft.EntityFrameworkCore;
using Optional;
using Optional.Unsafe;

namespace Demo.CQRS.Infrastructure
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private Func<EnrollmentContext> _contextFactory;
        public EnrollmentRepository(Func<EnrollmentContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            using var context = _contextFactory();

            var enrollments = await context.CourseEnrollments.ToListAsync();
            
            return enrollments.Select(e => new Domain.Enrollment
            {
                Id = e.Id,
                UserId = e.UserId,
                Course = new Course { Id = e.CourseId },
                ContactEmail = e.ContactEmail,
                CanceledTimestamp = e.CanceledTimestamp.ToOption(),
                EnrollmentTimestamp = e.EnrollmentTimestamp,
            });
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

        public async Task<Guid> UpdateAsync(Enrollment enrollment)
        {
            using var context = _contextFactory();

            var enrollmentEntity = new CourseEnrollment
            {
                Id = enrollment.Id,
                UserId = enrollment.UserId,
                ContactEmail = enrollment.ContactEmail,
                CourseId = enrollment.Course.Id,
                EnrollmentTimestamp = enrollment.EnrollmentTimestamp,
                CanceledTimestamp = enrollment.CanceledTimestamp.ToNullable()
            };

            var entry = context.CourseEnrollments.Update(enrollmentEntity);

            await context.SaveChangesAsync();

            return entry.Entity.Id;
        }
    }
}
