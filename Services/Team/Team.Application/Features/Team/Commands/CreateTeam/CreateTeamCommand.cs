using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.Team.Commands.CreateTeam
{
    public record CreateTeamCommand(string Name, TimeSpan AppointmentDuration, int AmountOfAppointments, HashSet<Guid> OpeningHoursIds) 
        : IRequest<Guid>;
}
