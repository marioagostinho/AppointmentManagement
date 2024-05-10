using AutoMapper;
using MediatR;
using Team.Domain.Repositories;
using Entities = Team.Domain.Entities;

namespace Team.Application.Features.OpeningTimeSlot.Commands.UpdateOpeningTimeSlot
{
    public class UpdateOpeningTimeSlotCommandHandler : IRequestHandler<UpdateOpeningTimeSlotCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public UpdateOpeningTimeSlotCommandHandler(IMapper mapper, IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
            _mapper = mapper;
            _openingTimeSlotRepository = openingTimeSlotRepository;
        }

        public async Task<bool> Handle(UpdateOpeningTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOpeningTimeSlotCommandValidator(_openingTimeSlotRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            var openingTimeSlot = _mapper.Map<Entities.OpeningTimeSlot>(request);
            bool result = await _openingTimeSlotRepository.UpdateAsync(openingTimeSlot);

            return result;
        }
    }
}
