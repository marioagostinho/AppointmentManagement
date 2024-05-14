using Appointment.Application.Features.Appointment.Queries.GetAppointments;

namespace Appointment.Application.UnitTests.Features.Appointment.Queries
{
    public class GetAppointmentsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;

        public GetAppointmentsQueryHandlerTests()
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
        public async Task Handle_ReturnsAllAppointmentsSuccessfully()
        {
            // Arrange
            var handler = new GetAppointmentsQueryHandler(_mapper, _mockAppointmentRepository.Object);
            var query = new GetAppointmentsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<AppointmentDto>>();
            result.Count.ShouldBe(5);
        }
    }
}
