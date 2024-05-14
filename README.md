[![LinkedIn][linkedin-shield]][linkedin-url] [![Trello][trello-icon]][trello-url]

# Appointment Management

<br />

<p align="center">
  <img src="https://github.com/marioagostinho/AppointmentManagement/blob/main/architecture.PNG" />
  
</p>

<br />

This project was build with the goal of implementing a simples food inventory by batches.

<br />

### Built With

* .NET 8.0
* Microservices:
  - MediatR
  - Entity Framework
  - SQL Server
  - AutoMapper
  - FluentValidation
* API Gateway:
  - Ocelot API
* Message Broker:
  - MassTransit
  - RabbitMQ
* Tests:
  - xUnit
  - Moq
  - Shouldly
  - Entity Framework InMemory
* Docker

<br />

## Getting Started

### Prerequisites

You will need to have installed:
1. **.NET SDK 8**
2. **Docker Desktop**

### Installation

Clone the repository
 ```sh
 git clone https://github.com/marioagostinho/AppointmentManagement.git
 ```

### Starting

From the cloned repository execute
 ```sh
 docker-compose up --build
 ```

<br />

and voilà the application is ready to be used :slightly_smiling_face:

<br />

## Roadmap

- [ ] Add more testing
- [ ] Add exception treatment
- [ ] Add async validation when creating/ editing appointments
- [ ] Add client


<!-- VARS -->

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=0077b5
[linkedin-url]: https://www.linkedin.com/in/mario-agostinho-5b364912b/
[trello-icon]: https://img.shields.io/badge/-Trello-black.svg?style=for-the-badge&logo=trello&colorB=0052CC
[trello-url]: https://trello.com/b/GzIMESyx/appointment-management
