using Appointment.Application.Features.Appointment.Commands.UpdateAppointment;

namespace Appointment.Application.UnitTests.Features.Appointment.Commands
{
    public class UpdateAppointmentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;

        public UpdateAppointmentCommandHandlerTests()
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
        public async Task Handle_ReturnsTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var handler = new UpdateAppointmentCommandHandler(_mapper, _mockAppointmentRepository.Object);
            var command = new UpdateAppointmentCommand(
                Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7"),
                MockAppointmentRepository.ConstructDate(DateTime.Now, 1, new TimeSpan(10, 0, 0)),
                TimeSpan.FromMinutes(15),
                Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<bool>();
            result.ShouldBeEquivalentTo(true);
        }

        [Fact]
        public async Task Handler_ThrowsException_WhenAppointmentDoesNotExist()
        {
            // Arrange
            var handler = new UpdateAppointmentCommandHandler(_mapper, _mockAppointmentRepository.Object);
            var command = new UpdateAppointmentCommand(
                Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b9"),
                MockAppointmentRepository.ConstructDate(DateTime.Now, 1, new TimeSpan(10, 0, 0)),
                TimeSpan.FromMinutes(15),
                Guid.Parse("4a7d1ed4-60af-4a8b-bd88-59fc9d132ba0")
            );

            // Act & Assert
            await Should.ThrowAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
