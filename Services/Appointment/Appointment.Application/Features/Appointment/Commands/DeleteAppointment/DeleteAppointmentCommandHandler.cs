using Appointment.Domain.Repositories;
using MediatR;

namespace Appointment.Application.Features.Appointment.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);

            if (appointment == null)
                throw new Exception();

            var result = await _appointmentRepository.DeleteAsync(appointment);

            return result;
        }
    }
}
