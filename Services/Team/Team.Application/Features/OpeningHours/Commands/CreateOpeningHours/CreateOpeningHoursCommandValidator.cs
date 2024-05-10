using FluentValidation;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningHours.Commands.CreateOpeningHours
{
    public class CreateOpeningHoursCommandValidator : AbstractValidator<CreateOpeningHoursCommand>
    {
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public CreateOpeningHoursCommandValidator(IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
           _openingTimeSlotRepository = openingTimeSlotRepository;

            RuleFor(p => p.DayOfWeek)
                .NotNull();

            RuleFor(p => p.OpeningTimeSlotIds)
                .NotNull()
                .MustAsync(AllOpeningTimeSlotMustExist).WithMessage("{PropertyName} has invalid values");
        }

        public async Task<bool> AllOpeningTimeSlotMustExist(HashSet<Guid> openingTimeSlotIds, CancellationToken cancellationToken)
        {
            var existingOpeningTimeSlot = await _openingTimeSlotRepository.GetByIdRangeAsync(openingTimeSlotIds);

            if (existingOpeningTimeSlot.Count != openingTimeSlotIds.Count)
                return false;

            var validOpeningTimeSlotIds = new HashSet<Guid>(existingOpeningTimeSlot.Select(p => p.Id));
            var invalidOpeningTimeSlotIds = openingTimeSlotIds.Where(p => !validOpeningTimeSlotIds.Contains(p)).ToList();

            return invalidOpeningTimeSlotIds.Count == 0; ;
        }
    }
}
