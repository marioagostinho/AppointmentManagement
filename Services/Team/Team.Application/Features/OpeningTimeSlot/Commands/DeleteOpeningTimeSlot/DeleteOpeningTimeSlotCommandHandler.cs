using AutoMapper;
using MediatR;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningTimeSlot.Commands.DeleteOpeningTimeSlot
{
    public class DeleteOpeningTimeSlotCommandHandler : IRequestHandler<DeleteOpeningTimeSlotCommand, bool>
    {
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public DeleteOpeningTimeSlotCommandHandler(IMapper mapper, IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
            _openingTimeSlotRepository = openingTimeSlotRepository;
        }

        public async Task<bool> Handle(DeleteOpeningTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var openingTimeSlot = await _openingTimeSlotRepository.GetByIdAsync(request.Id);

            if (openingTimeSlot == null)
                throw new Exception();

            bool result = await _openingTimeSlotRepository.DeleteAsync(openingTimeSlot);

            return result;
        }
    }
}
