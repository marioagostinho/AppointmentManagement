using Appointment.Domain.Repositories;
using FluentValidation;

namespace Appointment.Application.Features.Appointment.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentCommandValidator(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(AppointmentMustExist);

            RuleFor(p => p.StartDateTime)
                .NotNull();

            RuleFor(p => p.Duration)
                .NotNull();

            RuleFor(p => p.TeamId)
                .NotNull();
        }

        private async Task<bool> AppointmentMustExist(Guid id, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            return appointment != null;
        }
    }
}
