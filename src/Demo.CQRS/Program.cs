using Demo.CQRS.BL.Facade;
using Microsoft.Extensions.DependencyInjection;
using Demo.CQRS;
using Demo.CQRS.DAL.Course;
using System;
using Demo.CQRS.DAL.Enrollment;

// Dependency Injection
// Call GetService<> if you want to get something
using var dependencies = new Dependencies();
var serviceProvider = dependencies.ServiceProvider;

var facade = serviceProvider.GetService<ICoursesFacade>();
var courses = await facade.GetOpenCoursesIn("Newport");
foreach(var course in courses)
{
    Console.WriteLine($"{course.Name}");
}
var userId = Guid.NewGuid();

await facade.Enroll(courses.First(), userId, "user@example.test");

using var context = serviceProvider.GetService<Func<EnrollmentContext>>().Invoke();
context.CourseEnrollments.First();
Console.WriteLine($"Enrollement: {context.CourseEnrollments.First().Id}");