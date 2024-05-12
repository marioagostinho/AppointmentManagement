﻿using FluentValidation;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningHours.Queries.GetOpeningHoursByTeamDate
{
    public class GetOpeningHoursByTeamDateQueryValidator : AbstractValidator<GetOpeningHoursByTeamDateQuery>
    {
        private readonly ITeamRepository _teamRepository;

        public GetOpeningHoursByTeamDateQueryValidator(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;

            RuleFor(p => p.TeamId)
                .NotNull()
                .MustAsync(TeamMustExist).WithMessage("{PropertyName} must exist.");

            RuleFor(p => p.Date)
                .NotNull()
                .Must(date => date > DateTime.UtcNow).WithMessage("{PropertyName} must be later than now.");
        }

        private async Task<bool> TeamMustExist(Guid teamId, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(teamId);

            return team != null;
        }
    }
}
