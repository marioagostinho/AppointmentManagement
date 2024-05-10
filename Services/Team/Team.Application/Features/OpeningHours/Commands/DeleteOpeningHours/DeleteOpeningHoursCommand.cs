using MediatR;

namespace Team.Application.Features.OpeningHours.Commands.DeleteOpeningHours
{
    public record DeleteOpeningHoursCommand(Guid Id) : IRequest<bool>;
}
