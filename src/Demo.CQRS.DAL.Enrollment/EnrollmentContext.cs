using Microsoft.EntityFrameworkCore;

namespace Demo.CQRS.DAL.Enrollment
{
    public class EnrollmentContext : DbContext
    {
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }

        public EnrollmentContext(DbContextOptions<EnrollmentContext> options) : base(options)
        {
        }
    }
}