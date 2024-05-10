using MediatR;
using Team.Application.Dtos;

namespace Team.Application.Features.OpeningHours.Queries.GetOpeningHoursDetails
{
    public record GetOpeningHoursDetailsQuery(Guid Id) : IRequest<OpeningHoursDto>;
}
