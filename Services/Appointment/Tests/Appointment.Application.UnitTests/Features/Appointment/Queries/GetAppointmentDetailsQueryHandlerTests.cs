using Appointment.Application.Features.Appointment.Queries.GetAppointmentDetails;

namespace Appointment.Application.UnitTests.Features.Appointment.Queries
{
    public class GetAppointmentDetailsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;

        public GetAppointmentDetailsQueryHandlerTests()
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
        public async Task Handle_ReturnsAppointmentSuccessfully_WhenIdExists()
        {
            // Arrange
            var id = Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7");
            var expectedAppointment = new AppointmentDto(
                Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7"),
                MockAppointmentRepository.ConstructDate(DateTime.Now, 1, new TimeSpan(8, 0, 0)),
                TimeSpan.FromMinutes(15),
                Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
            );

            var handler = new GetAppointmentDetailsQueryHandler(_mapper, _mockAppointmentRepository.Object);
            var query = new GetAppointmentDetailsQuery(id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<AppointmentDto>();
            result.ShouldBeEquivalentTo(expectedAppointment);
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenIdDoesNotExists()
        {
            // Arrange
            var id = new Guid();

            var handler = new GetAppointmentDetailsQueryHandler(_mapper, _mockAppointmentRepository.Object);
            var query = new GetAppointmentDetailsQuery(id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }
    }
}
