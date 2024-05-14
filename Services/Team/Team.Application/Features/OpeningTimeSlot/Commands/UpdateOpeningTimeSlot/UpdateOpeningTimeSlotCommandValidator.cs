using FluentValidation;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningTimeSlot.Commands.UpdateOpeningTimeSlot
{
    public class UpdateOpeningTimeSlotCommandValidator : AbstractValidator<UpdateOpeningTimeSlotCommand>
    {
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public UpdateOpeningTimeSlotCommandValidator(IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
            _openingTimeSlotRepository = openingTimeSlotRepository;

            RuleFor(P => P.Id)
                .NotNull()
                .MustAsync(OpeningTimeSlotMustExist).WithMessage("{PropertyName} must exist");

            RuleFor(p => p.StartHour)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(p => p.EndHour)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();
        }

        private async Task<bool> OpeningTimeSlotMustExist(Guid id, CancellationToken cancellationToken)
        {
            var openingTimeSlot = await _openingTimeSlotRepository.GetByIdAsync(id);

            return openingTimeSlot != null;
        }
    }
}
