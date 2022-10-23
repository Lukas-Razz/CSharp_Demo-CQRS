using Demo.CQRS.BL.Behavior;
using Demo.CQRS.BL.Commands.EnrollToCourse;
using Demo.CQRS.BL.Contracts;
using Demo.CQRS.BL.Facade;
using Demo.CQRS.DAL.Course;
using Demo.CQRS.DAL.Enrollment;
using Demo.CQRS.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.CQRS
{
    // This sets up the appliation
    internal class Dependencies : IDisposable
    {
        private ServiceProvider _serviceProvider;
        private SqliteConnection _courseConnection;
        private SqliteConnection _enrollmentConnection;

        public ServiceProvider ServiceProvider
        {
            get => _serviceProvider;
        }

        public Dependencies()
        {
            _courseConnection = new SqliteConnection("Filename=:memory:");
            _courseConnection.Open();

            _enrollmentConnection = new SqliteConnection("Filename=:memory:");
            _enrollmentConnection.Open();

            _serviceProvider = RegisterDependencyInjection();

            SeedDatabase().GetAwaiter().GetResult();
        }

        private ServiceProvider RegisterDependencyInjection()
        {
            var services = new ServiceCollection();

            var courseContextObtions = new DbContextOptionsBuilder<CourseContext>()
                .UseSqlite(_courseConnection)
                .Options;
            services.AddSingleton<Func<CourseContext>>(() => new CourseContext(courseContextObtions));

            var enrollmentContextObtions = new DbContextOptionsBuilder<EnrollmentContext>()
                .UseSqlite(_enrollmentConnection)
                .Options;
            services.AddSingleton<Func<EnrollmentContext>>(() => new EnrollmentContext(enrollmentContextObtions));

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            services.AddOptions<EmailServiceConfiguration>().Configure(options =>
            {
                options.ApiKey = "this-is-not-a-key";
                options.Sender = "CourseDemo@example.test";
            });
            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IValidator<EnrollToCourseCommand>, EnrollToCourseCommandValidator>();

            services.AddMediatR(typeof(CoursesFacade)); // Registers handlers in Assembly with CoursesFacade

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            services.AddTransient<ICoursesFacade, CoursesFacade>();

            return services.BuildServiceProvider();
        }

        private async Task SeedDatabase()
        {
            var enrollmentContextFactory = _serviceProvider.GetService<Func<EnrollmentContext>>();
            using var enrollmentContext = enrollmentContextFactory.Invoke();
            enrollmentContext.Database.EnsureDeleted();
            enrollmentContext.Database.EnsureCreated();

            var courseContextFactory = _serviceProvider.GetService<Func<CourseContext>>();
            using var courseContext = courseContextFactory.Invoke();
            courseContext.Database.EnsureDeleted();
            courseContext.Database.EnsureCreated();

            courseContext.Courses.AddRange(new Course
            {
                Id = Guid.NewGuid(),
                Contact = "Gromit@example.test",
                Location = "Newport",
                Name = "Maintaining Sanity",
                Start = DateTime.UtcNow.AddDays(-7)
            },
            new Course
            {
                Id = Guid.NewGuid(),
                Contact = "Wallace@example.test",
                Location = "Newport",
                Name = "Inventing New Things",
                Start = DateTime.UtcNow.AddDays(7)
            },
            new Course
            {
                Id = Guid.NewGuid(),
                Contact = "Lukas@example.test",
                Location = "Brno",
                Name = "CQRS 101",
                Start = DateTime.UtcNow.AddDays(1)
            });

            await courseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _courseConnection.Dispose();
            _enrollmentConnection.Dispose();
        }
    }
}
