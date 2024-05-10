using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.OpeningHours.Queries.GetOpeningHours
{
    public record GetOpeningHoursQuery : IRequest<IReadOnlyList<OpeningHoursDto>>;
}
