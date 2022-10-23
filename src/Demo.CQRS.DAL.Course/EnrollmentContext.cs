using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Demo.CQRS.DAL.Course
{
    public class CourseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }
    }
}