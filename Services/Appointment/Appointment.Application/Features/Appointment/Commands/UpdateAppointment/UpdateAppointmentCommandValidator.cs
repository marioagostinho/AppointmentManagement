using FluentValidation;

namespace Appointment.Application.Features.Appointment.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull();

            RuleFor(p => p.StartDateTime)
                .NotNull();

            RuleFor(p => p.Duration)
                .NotNull();

            RuleFor(p => p.TeamId)
                .NotNull();
        }
    }
}
