using FluentValidation;

namespace Appointment.Application.Features.Appointment.Commands.CreateAppointment
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(p => p.StartDateTime)
                .NotNull();

            RuleFor(p => p.Duration)
                .NotNull();

            RuleFor(p => p.TeamId)
                .NotNull()
                .NotEmpty();
        }
    }
}
