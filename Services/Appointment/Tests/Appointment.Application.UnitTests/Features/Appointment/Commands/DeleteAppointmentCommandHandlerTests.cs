using Appointment.Application.Features.Appointment.Commands.DeleteAppointment;
using Appointment.Application.UnitTests.Mocks;
using Appointment.Domain.Repositories;
using Moq;
using Shouldly;

namespace Appointment.Application.UnitTests.Features.Appointment.Commands
{
    public class DeleteAppointmentCommandHandlerTests
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;

        public DeleteAppointmentCommandHandlerTests()
        {
            // Setting up the Mock
            _mockAppointmentRepository = MockAppointmentRepository.GetMockAppointmentRepository();
        }

        [Fact]
        public async Task Handle_ReturnsTrue_WhenDeletionIsSuccessful()
        {
            // Arrange
            var handler = new DeleteAppointmentCommandHandler(_mockAppointmentRepository.Object);
            var command = new DeleteAppointmentCommand(
                Guid.Parse("8a26e9b1-8d66-4f13-ba09-519fbc34c0b7")
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Asserts
            result.ShouldBeOfType<bool>();
            result.ShouldBeEquivalentTo(true);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenAppointmentDoesNotExist()
        {
            // Arrange
            var handler = new DeleteAppointmentCommandHandler(_mockAppointmentRepository.Object);
            var command = new DeleteAppointmentCommand(
                new Guid()
            );

            // Act & Assert
            await Should.ThrowAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
