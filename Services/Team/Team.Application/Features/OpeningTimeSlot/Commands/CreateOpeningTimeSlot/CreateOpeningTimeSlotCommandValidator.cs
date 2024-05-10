using FluentValidation;

namespace Team.Application.Features.OpeningTimeSlot.Commands.CreateOpeningTimeSlot
{
    public class CreateOpeningTimeSlotCommandValidator : AbstractValidator<CreateOpeningTimeSlotCommand>
    {
        public CreateOpeningTimeSlotCommandValidator()
        {
            RuleFor(p => p.StartHour)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(p => p.EndHour)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();
        }
    }
}
