using Appointment.Application.Features.Appointment.Commands.CreateAppointment;

namespace Appointment.Application.UnitTests.Features.Appointment.Commands
{
    public class CreateAppointmentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;

        public CreateAppointmentCommandHandlerTests()
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
        public async Task Handle_ReturnsGuid_WhenCreateAppointmentIsSuccessfull()
        {
            // Arrange
            var handler = new CreateAppointmentCommandHandler(_mapper, _mockAppointmentRepository.Object);
            var command = new CreateAppointmentCommand(
                MockAppointmentRepository.ConstructDate(DateTime.Now, 1, new TimeSpan(10, 0, 0)),
                TimeSpan.FromMinutes(15),
                Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<Guid>();
            result.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenValidationIsUnsuccessfull()
        {
            // Arrange
            var handler = new CreateAppointmentCommandHandler(_mapper, _mockAppointmentRepository.Object);
            var command = new CreateAppointmentCommand(
                MockAppointmentRepository.ConstructDate(DateTime.Now, 1, new TimeSpan(10, 0, 0)),
                TimeSpan.FromMinutes(15),
                Guid.Empty
            );

            // Act & Assert
            await Should.ThrowAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
