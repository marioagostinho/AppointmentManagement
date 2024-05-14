using Appointment.Application.MappingProfiles;
using Appointment.Application.UnitTests.Mocks;
using Appointment.Domain.Repositories;
using AutoMapper;
using Moq;
using Appointment.Application.Features.Appointment.Queries.GetAppointmentsByTeamAndDate;
using Appointment.Application.Dtos;
using Shouldly;
using static MassTransit.ValidationResultExtensions;

namespace Appointment.Application.UnitTests.Features.Appointment.Queries
{
    public class GetAppointmentsByTeamAndDateQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;

        public GetAppointmentsByTeamAndDateQueryHandlerTests()
        {
            // Setting up the AutoMapper 
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AppointmentProfile());
            });

            _mapper = configuration.CreateMapper();

            // Setting up the Mock
            _mockAppointmentRepository = MockAppointmentRepository.GetMockAppointmentRepository();
        }

        [Fact]
        public async Task Handle_ReturnsAppointmentsSuccessfully_WhenTeamIdAndDateExists()
        {
            // Arrange
            DateTime now = DateTime.Now;

            var teamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0");
            DateTime date = MockAppointmentRepository.ConstructDate(now, 1, new TimeSpan(8, 0, 0));

            var expectedAppointments = new List<AppointmentDto>
            {
                new AppointmentDto
                (
                    Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7"),
                    MockAppointmentRepository.ConstructDate(now, 1, new TimeSpan(8,0,0)),
                    TimeSpan.FromMinutes(15),
                    Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                ),
                new AppointmentDto
                (
                    Guid.Parse("ac3e602b-75ae-499d-9c75-29fbdfeb0f29"),
                    MockAppointmentRepository.ConstructDate(now, 1, new TimeSpan(8,15,0)),
                    TimeSpan.FromMinutes(15),
                    Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                ),
                new AppointmentDto
                (
                    Guid.Parse("5e35d8ac-4178-473a-89b5-b99c1746fd7c"),
                    MockAppointmentRepository.ConstructDate(now, 1, new TimeSpan(8,30,0)),
                    TimeSpan.FromMinutes(15),
                    Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
                )
            };

            var handler = new GetAppointmentsByTeamAndDateQueryHandler(_mapper, _mockAppointmentRepository.Object);
            var query = new GetAppointmentsByTeamAndDateQuery(teamId, date);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Asserts
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<AppointmentDto>>();
            result.Count.ShouldBe(3);
            result.ShouldBeEquivalentTo(expectedAppointments);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenTeamIdDoesNotExists()
        {
            // Arrange
            DateTime now = DateTime.Now;

            var teamId = Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0");
            DateTime date = MockAppointmentRepository.ConstructDate(now, 10, new TimeSpan(8, 0, 0));

            var handler = new GetAppointmentsByTeamAndDateQueryHandler(_mapper, _mockAppointmentRepository.Object);
            var query = new GetAppointmentsByTeamAndDateQuery(teamId, date);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Asserts
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<AppointmentDto>>();
            result.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenDateDoesNotExists()
        {
            // Arrange
            DateTime now = DateTime.Now;

            var teamId = new Guid();
            DateTime date = MockAppointmentRepository.ConstructDate(now, 1, new TimeSpan(8, 0, 0));

            var handler = new GetAppointmentsByTeamAndDateQueryHandler(_mapper, _mockAppointmentRepository.Object);
            var query = new GetAppointmentsByTeamAndDateQuery(teamId, date);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Asserts
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<AppointmentDto>>();
            result.Count.ShouldBe(0);
        }
    }
}
