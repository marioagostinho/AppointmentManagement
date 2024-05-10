using FluentValidation;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.Team.Commands.CreateTeam
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        private readonly IOpeningHoursRepository _openingHoursRepository;

        public CreateTeamCommandValidator(IOpeningHoursRepository openingHoursRepository)
        {
            _openingHoursRepository = openingHoursRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(p => p.AppointmentDuration)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(p => p.AmountOfAppointments)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(p => p.OpeningHoursIds)
                .MustAsync(AllOpeningHoursMustExist).WithMessage("{PropertyName} has invalid values");
        }

        public async Task<bool> AllOpeningHoursMustExist(HashSet<Guid> openingHoursIds, CancellationToken cancellationToken)
        {
            var existingOpeningHours = await _openingHoursRepository.GetByIdRangeAsync(openingHoursIds);

            if (existingOpeningHours.Count != openingHoursIds.Count)
                return false;

            var validOpeningHourIds = new HashSet<Guid>(existingOpeningHours.Select(p => p.Id));
            var invalidOpeningHoursIds = openingHoursIds.Where(p => !validOpeningHourIds.Contains(p)).ToList();

            return invalidOpeningHoursIds.Count == 0;
        }
    }
}
