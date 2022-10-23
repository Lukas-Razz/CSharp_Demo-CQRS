using Demo.CQRS.BL.Commands.EnrollToCourse;
using Demo.CQRS.BL.Queries;
using Demo.CQRS.Domain;
using MediatR;
using Optional;

namespace Demo.CQRS.BL.Facade
{
    // Facades are for creation of simplified APIs
    // They do not need to offer the full functionality, just the most importatnt things
    // You can frequently see it in libraries
    public class CoursesFacade : ICoursesFacade
    {
        private IMediator _mediator;
        public CoursesFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<Course>> GetOpenCoursesIn(string location)
        {
            var query = new GetCoursesQuery
            {
                After = DateTime.UtcNow.Some(),
                AtLocation = location.Some(),
            };
            return _mediator.Send(query);
        }

        public Task Enroll(Course course, Guid userId, string email)
        {
            var command = new EnrollToCourseCommand
            {
                Course = course,
                UserId = userId,
                ContactEmail = email
            };
            return _mediator.Send(command);
        }
    }
}
