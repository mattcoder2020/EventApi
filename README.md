# Instructions

Please stick to the following instructions on how to submit your application:

1. Read the **whole** README
2. Create a new repository on your Github Account. *Note: The repository has to be public.*
3. Add your solution **with all requirements** to your repository
4. Send a mail to [application@innoloft.com](mailto:application@innoloft.com) with following information:
   - Your Name
   - Link to **public** accessable repository on **GitHub**
   - How many hours it took to complete (roughly)

**Please do not spend much more than 6 hours for the whole task.** This is not a hard limitation but want to respect your time since we cannot hire every applicant. Also only start with the task if you think this is something you can do in the given time frame.

### Additional Information to submit a successful application

- Make sure that your repository is public
- Only an application with [all requirements](https://github.com/innoloft/Frontend-Application#technical-requirements) can be considered
- Provide setup process if required
- Copied structures or code from other applications will be completely ignored

Thank you very much and have fun with the challenge!

# Challenge

Develop a minified version of the Events module API based on wireframes. The API should provide endpoints for events CRUD.

It also should enable users to register for the the event.

### 1. List of current user’s events

![Event Listing](<assets/image_6.png>)

### 2. Creating & editing of event

![Event Creation](<assets/image_1.png>)

![Event Creation - Date Picking](<assets/image_2.png>)

### 3. Event info and participants registration

![Event Information](<assets/image_3.png>)

## Clarification

- You **do not** have to build the frontend. Only the API requests that would enable the frontend to work.
- Top navigation, menu on the left, etc. are not part of this task. This is only about the events module.
- Pagination should be added in GET all events to enable loading more events

# Technical Requirements

- Project
  - [ASP.NET Core web API application](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio). Prefer version 6.
- Database
  - Use SQLite or MySQL (**Do not use Microsoft SQL or SQL Express**). Please add setup instructions if necessary
  - Use EF Core ORM framework to work with database
- Tests project
  - At least one unit test should be written (even the simplest one)
- Project should be setup to run as a docker container - `Dockerfile` is required
- Attach user data
  - To get complete single event page response - it should include the user data as on the mockup. Make an API request to user API to get user info ([https://jsonplaceholder.typicode.com/users/1](https://jsonplaceholder.typicode.com/users/1))

## Bonus Points

- Use caching
- Use Rich domain model
- Add a diagram of the solution in Miro, attach screenshot in repository
- Cover solution classes with unit tests

❗ Please add instructions for setup if necessary

# Bonus Feature

Implement invitations management mechanics:

Event creator should be able to invite participants. Then, after approval, the invited user will become an event participant.

❗No need to implement user management endpoints like fetching all users. Assume frontend will work with users API and send you selected userIds.

### Invitations management

![Invitation Listing](<assets/image_4.png>)

![Received Invitation](<assets/image_5.png>)


# Application Code Structure and layers
The code is structured in the following way:
- Controllers: Contains the event api endpoints for events CRUD and command that add invite, approve invite, and add participant to events
               It also leverage the builtin dependency injection to inject the services
- DomainModels: Contains the models used in the application
          Event.cs: Contains the event model which is the aggregate root that manage the life cycle of invitation and the participants as its aggregate members
          Invitation.cs: Contains the invitation model which is the aggregate member of the event
          Participant.cs: Contains the participant model which is the aggregate member of the event
- Services: interface based service that decouple the API controller from data access layer and domain model to ensure its testability, it also support query that return paginated results 
- Infrastruture/DataAccess: Contains the db context class that serve as ORM for the domain models with capability to seed the database with sample data for a quick demo purpose
- Infrastruture/Repository: A generic repository class that encapsulate the db context and provide the basic CRUD operations and eager loading methods for the domain models,
                            This saves the effort to write seperate repository class for each domain model.
- Infrastruture/Cache: For simplicity I did not use distributed cache but rather leverage the builtin memory cache, the main focus is to cache the events data which are frequently visit, 
                       the cache is invalidate when there is a change (CUD) in the events data 
- Infrastruture/Middleware: Custom middleware that serve as global exception handler to ensure the service doesnt break for any unexpected error and return the appropriate response to end user
- Infrastruture/Logging: To Be Implemented
- Exceptions: Contains the custom exceptions used in the services for fine grained exception handling

# Application Design
The application is designed using the following patterns:
- Repository: The application is designed using repository pattern to decouple the data access layer from the domain model and the API controller, this is to ensure the testability of the service layer
- Mediator: The application is designed using mediator pattern to decouple the API controller from the service layer, this is to ensure the testability of the service layer
- Dependency Injection: The application is designed using dependency injection pattern to decouple the API controller from the service layer, this is to ensure the testability of the service layer
- Cache Aside: The application is designed using cache aside pattern to cache the frequently visit events data, this is to improve the performance of the application
- Domain Driven Design: The application is designed using domain driven design to ensure the data integrity of the application

# Application Testability
The application is designed to be testable in the following ways:
- Unit Test: The application is designed using repository pattern to decouple the data access layer from the domain model and the API controller, this is to ensure the testability of the service layer
- Integration Test: The application is designed using repository pattern to decouple the data access layer from the domain model and the API controller, this is to ensure the testability of the service layer
- Mocking: The application is designed using repository pattern to decouple the data access layer from the domain model and the API controller, this is to ensure the testability of the service layer
- Dependency Injection: The application is designed using dependency injection pattern to decouple the API controller from the service layer, this is to ensure the testability of the service layer

# Test Project
The test project is structured in the following way:
- Unit Test: Contains the unit test for the API controller and service layer implementations
- Integration Test: Not Implemented but will needed to test the application end to end on DB and Caching for any given user case senario.

# Diagram
![Diagram](<assets/diagram.png>)
# Setup
- Clone the repository
- Open the solution in Visual Studio
- Build the solution
- Run the application
- Access the API: 
	 http://localhost:5000/api/events
- Access the Swagger UI:
http://localhost:5000/swagger/index.html

# Setup and Deployment
- Clone the repository
- Create docker image: 
     docker build . -t eventapi/eventapiservice -f dockerfile
Run docker image:  
     docker run -p 8080:80 eventapi/eventapiservice
Access the API: 
	 http://localhost:8080/api/events

# Quick API Demo