* Do not delve into to much details about the demo project. Focus on the important things.
* Both DAL projects are placeholders. In reality those could be whole different technologies.
* Repositories are simple and incomplete. They are not the topic of this demo.
* Persistence-level and Domain-level entities are separated. Domain should not care how it is stored.
* Contracts (interfaces) are located at BL. It does not care how it is implemented, but know what it needs.
* ValidationBehavior is there to demonstrate the MediatR ability to model cross-cutting concerns. Do not spend too much time there.
* Same for the EnrollToCourseCommandValidator. But you can mention that something like FluentValidation exists.
* EmailService is just to show that single command can do more things.
* Take the dependency injection as it is. Everything is set for the tasks. You don't have to modify the registration.