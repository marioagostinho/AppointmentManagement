using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.Team.Commands.UpdateTeam
{
    public record UpdateTeamCommand(Guid Id, string Name, TimeSpan AppointmentDuration, int AmountOfAppointments, HashSet<Guid> OpeningHoursIds)
        : IRequest<bool>;
}
