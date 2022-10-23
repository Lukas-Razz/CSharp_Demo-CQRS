using Demo.CQRS.BL.Contracts;
using Demo.CQRS.DAL.Course;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Demo.CQRS.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private Func<CourseContext> _contextFactory;
        public CourseRepository(Func<CourseContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Domain.Course>> GetAllAsync()
        {
            using var context = _contextFactory();

            var courses = await context.Courses.ToListAsync();

            return courses.Select(c => new Domain.Course
            {
                Id = c.Id,
                Name = c.Name,
                Contact = c.Contact,
                Location = c.Location,
                Start = c.Start
            });
        }
    }
}
