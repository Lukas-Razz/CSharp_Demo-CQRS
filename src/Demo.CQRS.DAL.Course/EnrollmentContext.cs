using Microsoft.EntityFrameworkCore;

namespace Demo.CQRS.DAL.Course
{
    public class CourseContext : DbContext
    {
        public DbSet<Course> courses;

        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }
    }
}