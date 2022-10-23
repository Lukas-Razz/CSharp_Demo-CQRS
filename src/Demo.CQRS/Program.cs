using Microsoft.Extensions.DependencyInjection;
using Demo.CQRS;
using Optional;
using MediatR;
using Demo.CQRS.BL.Queries.GetCourses;
using Demo.CQRS.BL.Facade;

// Dependency Injection
// Call GetService<> if you want to get something
using var dependencies = new Dependencies();
var serviceProvider = dependencies.ServiceProvider;

// Try your implementation here

// Without facade
var query = new GetCoursesQuery
{
    After = DateTime.UtcNow.Some(),
    AtLocation = "Brno".Some(),
};
var courses = await serviceProvider.GetService<IMediator>().Send(query);
foreach (var course in courses)
{
    Console.WriteLine($"{course.Name}");
}

// With Facade
var userId = Guid.NewGuid();
var facade = serviceProvider.GetService<ICoursesFacade>();

// Get Courses
var courses2 = await facade.GetOpenCoursesIn("Newport");
foreach (var course in courses2)
{
    Console.WriteLine($"{course.Id}:{course.Name}");
}

// Enroll 
await facade.Enroll(courses2.First(), userId, "user@example.test");

// Get Enrolls
var enrollments = await facade.GetPendingEnrollements(userId);
foreach (var enrollment in enrollments)
{
    Console.WriteLine($"{enrollment.Course.Id}");
}

// Cancel 
await facade.CancelEnroll(enrollments.First());

// Get Enrolls
var enrollments2 = await facade.GetPendingEnrollements(userId);
foreach (var enrollment in enrollments2)
{
    Console.WriteLine($"{enrollment.Course.Id}");
}