# Prerequisite:
- .Net6.0
- Docker (docker desktop for windows)
- Postman
- Visual Studio 2022 or Visual Studio Code (optional)
- SQLiteStudio (optional)

# EventApi Project
 - EventApi is a simple REST API that allows users to create, update, delete, and view events, in addition the event organizer can send invitation to users 
    and add them as event participants when the invitation has been accepted.

# TestEventApi Project
The test project is structured in the following way:
- Unit Test: Contains the unit test for the REST API controller and service layer implementations
- Integration Test: Not Implemented but is needed to test the application end to end on DB and Caching for any given user case senario.

# Application Code Structure and layers
The code is structured in the following way:
- Controllers: Contains the event api endpoints for events CRUD and command that add invite, approve invite, and add participant to events
               It also leverage the builtin dependency injection to inject the services
- DomainModels: Contains the models used in the application
          Event.cs: Contains the event model which is the aggregate root that manage the life cycle of invitation and the participants as its aggregate members
          Invitation.cs: Contains the invitation model which is the aggregate member of the event
          Participant.cs: Contains the participant model which is the aggregate member of the event
- Services: interface based service that decouple the API controller from data access layer and domain model to ensure its testability, it also support query that return paginated results 
- Exceptions: Contains the custom exceptions used in the services for fine grained exception handling
- Infrastruture/DataAccess: Contains the db context class that serve as ORM for the domain models with capability to seed the SQLite database with sample data for a quick demo purpose
- Infrastruture/Repository: A generic repository class that encapsulate the db context and provide the basic CRUD operations and eager loading methods for the domain models,
                            This saves the effort to write seperate repository class for each domain model.
- Infrastruture/Cache: For simplicity I did not use distributed cache but rather leverage the builtin memory cache, the main focus is to cache the events data which are frequently visit, 
                       the cache is invalidate when there is a change (CUD) in the events data 
- Infrastruture/Middleware: Custom middleware that serve as global exception http handler to ensure the service doesnt break for any unexpected error and return the appropriate response to end user
- Infrastruture/Logging: Not Implemented but is needed for logging purpose.

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


# Diagram
[Class Diagram](https://github.com/mattcoder2020/EventApi/blob/master/Assets/Class%20Diagram.jpg)
[Sequence Diagram](https://github.com/mattcoder2020/EventApi/blob/master/Assets/Entity%20Relationship%20Diagram.jpg)
[ER Diagram](https://github.com/mattcoder2020/EventApi/blob/master/Assets/Class%20Diagram.jpg)

# Setup and Deployment
- CD to root directory, run following command to clone the repository to local machine: 
	 git clone https://github.com/mattcoder2020/EventApi.git 
- Run following command to create docker image: 
     docker build . -t eventapi/eventapiservice -f dockerfile
- Run following command to create docker container from docker image:  
     docker run -p 8080:80 eventapi/eventapiservice
- Sanity test the API: 
	 http://localhost:8080/api/events

# Quick API Demo
- Open Postman and import the following collection:
- Open path - File->Setting->Genernal and disable SSL certificate verification
