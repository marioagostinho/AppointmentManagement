using FluentValidation;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningHours.Commands.UpdateOpeningHours
{
    public class UpdateOpeningHoursCommandValidator : AbstractValidator<UpdateOpeningHoursCommand>
    {
        private readonly IOpeningHoursRepository _openingHoursRepository;
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public UpdateOpeningHoursCommandValidator(IOpeningHoursRepository openingHoursRepository, IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
            _openingHoursRepository = openingHoursRepository;
            _openingTimeSlotRepository = openingTimeSlotRepository;

            RuleFor(P => P.Id)
                .NotNull()
                .MustAsync(OpeningHoursMustExist).WithMessage("{PropertyName} must exist");

            RuleFor(p => p.DayOfWeek)
                .NotNull();

            RuleFor(p => p.OpeningTimeSlotIds)
                .NotNull()
                .MustAsync(AllOpeningTimeSlotMustExist).WithMessage("{PropertyName} has invalid values");
        }

        private async Task<bool> OpeningHoursMustExist(Guid id, CancellationToken cancellationToken)
        {
            var openingHours = await _openingHoursRepository.GetByIdAsync(id);

            return openingHours != null;
        }

        public async Task<bool> AllOpeningTimeSlotMustExist(HashSet<Guid> openingTimeSlotIds, CancellationToken cancellationToken)
        {
            var existingOpeningTimeSlot = await _openingTimeSlotRepository.GetByIdRangeAsync(openingTimeSlotIds);

            if (existingOpeningTimeSlot.Count != openingTimeSlotIds.Count)
                return false;

            var validOpeningTimeSlotIds = new HashSet<Guid>(existingOpeningTimeSlot.Select(p => p.Id));
            var invalidOpeningTimeSlotIds = openingTimeSlotIds.Where(p => !validOpeningTimeSlotIds.Contains(p)).ToList();

            return invalidOpeningTimeSlotIds.Count == 0;
        }
    }
}
