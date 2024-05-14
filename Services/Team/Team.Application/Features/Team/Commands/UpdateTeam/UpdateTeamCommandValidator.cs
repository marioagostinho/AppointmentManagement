using FluentValidation;
using Team.Application.Dtos;
using Team.Domain.Repositories;

namespace Team.Application.Features.Team.Commands.UpdateTeam
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IOpeningHoursRepository _openingHoursRepository;

        public UpdateTeamCommandValidator(ITeamRepository teamRepository, IOpeningHoursRepository openingHoursRepository)
        {
            _teamRepository = teamRepository;
            _openingHoursRepository = openingHoursRepository;

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(TeamMustExist).WithMessage("{PropertyName} must exist");

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

        private async Task<bool> TeamMustExist(Guid id, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(id);

            return team != null;
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
